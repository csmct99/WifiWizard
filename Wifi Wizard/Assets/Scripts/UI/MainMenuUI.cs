﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void BeginGame() {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single); //Close current scene and begin loading the new one.
    }

    public void ExportData(){
        
    }
}
