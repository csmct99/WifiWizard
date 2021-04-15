using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(UIController))]
public class GameManager : MonoBehaviour {

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void DownloadToFile(string content, string filename, string contentType);
    #endif

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

    public void SendScoresheet(string msg){
        Debug.Log("Sending file to user with data: " + msg);

#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadToFile(msg, "my-new-file.txt", "text/plain");
#endif
    }
}
