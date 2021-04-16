Wifi Wizard

Unity - 2020.1.17f1
Python - 3.7+
	- You need pycryptodome on the machine decrypting files.
		Install with "pip install pycryptodome" in commandline. 
		You need python installed to have access to the pip command

In order to use the Decrypter.py file, use the command line
	> py Decrypter.py "C:/Foldername/filename.score"
		Decrypts the score of this single file

	> py Decrypter.py "C:/Foldername" 
		Decrypts all score files found in this folder

Note that the index build only works on an actual server. 
Running the webpage locally through your browser is hit or miss.
This is common with UnityWebGL projects, likely due to how they write it.