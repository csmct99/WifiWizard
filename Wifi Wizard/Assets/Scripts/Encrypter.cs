using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Text;


public class Encrypter : MonoBehaviour {
    public void Start()
    {
        string plain = "{'test':'test', 'test2':'test2'}";
        string encrypted = Encrypt<AesManaged>(plain, "LbumPXV9qSEBTkjxLafCzRQRk5tR4xnv", "VfsqM67o6g1JJ1coTDADu28GIhDKx9JV");

        Debug.Log(encrypted);
    }

    public static string Encrypt<T>(string value, string password, string salt)
         where T : SymmetricAlgorithm, new()
    {
        DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

        SymmetricAlgorithm algorithm = new T();

        byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
        byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
        Debug.Log("Key: " + Convert.ToBase64String(rgbKey, 0, rgbKey.Length));
        Debug.Log("Salt: " + Convert.ToBase64String(rgbIV, 0, rgbIV.Length));



        ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

        using (MemoryStream buffer = new MemoryStream())
        {
            using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                {
                    writer.Write(value);
                }
            }

            return Convert.ToBase64String(buffer.ToArray());
        }
    }



}