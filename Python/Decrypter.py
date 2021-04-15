import base64, sys
import Crypto.Cipher.AES
import json
from os import listdir
from os.path import isfile, join

def Decrypt(value):
    password = base64.b64decode('fiBSzZHjhgjOi0FPNNrrF5AIj7mSCOXJDH5aV0NDtQA=') # This value is fixed in Unity binaries and in this
    salt = base64.b64decode('l1EiOF1emAoYJrErdlWDCg==') # This value is fixed in Unity binaries and in this
    text = base64.b64decode(value)

    aes = Crypto.Cipher.AES.new(password, Crypto.Cipher.AES.MODE_CBC, salt)

    result = aes.decrypt(text).decode('utf-16')

    #Parse out non JSON data. Mainly the bad data after converting from utf-16
    batch = ""
    for s in result:
        batch += s
        if s == '}':
            break;

    return batch

path = sys.argv[1]

if (path[-6:] == ".score"):

    print("Reading data in file: " + path)

    file = open(path, "r")
    key = file.read()
    student = json.loads(Decrypt(key))

    print("Student name: " + student['student'])
    print("Student number: " + student['number'])
    print("Student score: " + student['score'])
else:
    print("Reading all files in directory: " + path)
    try:
        files = [f for f in listdir(path) if isfile(join(path, f))]
        for file in files:
            try:
                if(file[-6:] == ".score"): #Found a score file
                    try:
                        #print("Got score file");
                        f = open(path + "\\"+file, "r")
                        key = f.read()
                        student = json.loads(Decrypt(key))

                        print(student['student'] + "(" + student['number'] + ") " + student['score'])
                    except:
                        print("Failed to open file (is it corrupted?): " + file)
            except:
                print("Failed to read file: " + file)
    except:
        print("Could not read files in the given directory")

