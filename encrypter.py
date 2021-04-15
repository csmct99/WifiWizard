from cryptography.fernet import Fernet

key = b"qC3eSNcWvEj5y1OAOkr8y1E0i8TWEsaiHTCGZQhihOE="  # store in a secure location
#print("Key:", key.decode())

def encrypt(message: bytes, key: bytes) -> bytes:
    return Fernet(key).encrypt(message)

def decrypt(token: bytes, key: bytes) -> bytes:
    return Fernet(key).decrypt(token)


print(decrypt(b"gAAAAABgd3wBqDpVoNjIHdvSZoPK7S5a73-ybsmWUFZ31E-YFQLdccJIpy5Z6Ob_3vwT-kpqu0cjxPy9jNdwrhEy0LaX8cAwLg==", key));

#x = encrypt("Test_value_is_a_test".encode('utf-8'), key)
#print( x);

#y = decrypt(x,key)
#print(y);



#key = b'cw_0x689RpI-jtRR7oE8h_eQsKImvJapLeSbXpwF4e4='
#cipher_suite = Fernet(key)
#ciphered_text = cipher_suite.encrypt(b"TestErorr is a test value")
#print(ciphered_text)