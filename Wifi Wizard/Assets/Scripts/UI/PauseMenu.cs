using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public GameObject inputScreen;
    public TMP_InputField studentName;
    public TMP_InputField studentNumber;

    // Start is called before the first frame update
    void Start() {
        if(inputScreen == null || studentName == null || studentNumber == null){
            Debug.LogError("A referance on the pausemenu logic script on the canvas is not set");
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Resume(){
        gameObject.SetActive(false);
        GameManager.GamePaused = false;
    }

    public void Retry(){
        //Reload Level
        gameObject.SetActive(false);
        GameManager.GamePaused = false;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void Quit(){
        //Load main menu
        gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single); //unloads the current world and loads up the main menu
        //GameManager.GamePaused = false;
    }

    public void OpenInputScreen(){
        inputScreen.SetActive(true);
    }

    public void CloseInputScreen(){
        inputScreen.SetActive(false);
    }

    public void ExportScore(){
        string sNumber = studentNumber.text;
        string sName = studentName.text;
        GameManager.ExportScore(sNumber, sName);
    }
}
