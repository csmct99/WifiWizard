using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Text;

[RequireComponent(typeof(UIController))]
public class GameManager : MonoBehaviour {

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void DownloadToFile(string content, string filename, string contentType);
    #endif

    private static float highscore = 0;

    public static bool GamePaused = false;
    
    public UIController UI = null;

    // Start is called before the first frame update
    void Start() {

        //DontDestroyOnLoad(gameObject); //Makes this object immortal ...

        if(UI == null){
            UI = GetComponent<UIController>();
        }
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public static GameManager Find(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        //Debug.Log("Searching for gm ... found: " + (gm != null));
        
        if(gm == null){ //Still cant find canvas. Prob doesnt exist or its in the wrong heigharchy location.
            Debug.LogError("GameManager or UIController not found. \nAdd them to the level from the prefabs folder ('GameManager' object).");
            return null;
        }else{
            return gm;
        }
    }

    public static UIController FindUI(){
        return Find().gameObject.GetComponent<UIController>();
    }

    public static void UpdateScore(float score){
        if(score > highscore){
            highscore = score;
        }
    }

    public static float GetHighscore(){
        return highscore;
    }

    public static void SendScoresheet(string msg, string filename){
        Debug.Log("Sending file to user with data: " + msg);

#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadToFile(msg, filename + ".score", "text/plain");
#endif
    }

    public static void ExportScore(string studentNumber, string studentName){
        
        Debug.Log("Snum: " + studentNumber + " sName: " + studentName);
        string json = "{\"student\":\"" + studentName +"\",\"number\":\"" + studentNumber + "\",\"score\":\"" + highscore + "\"}"; //Creates the JSON based on the user's input
        
        string encryptedJson = AESEncrypter.Encrypt<AesManaged>(json, "LbumPXV9qSEBTkjxLafCzRQRk5tR4xnv", "VfsqM67o6g1JJ1coTDADu28GIhDKx9JV");
        SendScoresheet(encryptedJson, studentNumber);
    }
}

public class AESEncrypter
{
    /*
    public void Start()
    {
        string plain = "{'test':'test', 'test2':'test2'}";
        string encrypted = Encrypt<AesManaged>(plain, "LbumPXV9qSEBTkjxLafCzRQRk5tR4xnv", "VfsqM67o6g1JJ1coTDADu28GIhDKx9JV");

        Debug.Log(encrypted);
    }*/

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
