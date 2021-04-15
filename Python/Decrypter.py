import base64, sys
import Crypto.Cipher.AES



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

print(Decrypt('3M2C0LJH38o0gAvH6dEYfUhcIQG2TKG/1G3Hd+FxsKQOKuTVy3VlKCFyRBI7R9wQzADAvWOLC03ilm+r8Llx9Qs5S3FdlewlyKLXbTESZRE='))
