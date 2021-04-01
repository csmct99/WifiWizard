using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
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
}
