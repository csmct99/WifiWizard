using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour {

    [SerializeField] private GameObject ap1;
    [SerializeField] private GameObject ap2;
    [SerializeField] private GameObject ap3;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIController UI;

    public List<AccessPoint> contents = new List<AccessPoint>(); //Make empty inventory contents.

    private void Start(){
        contents.Add(new AccessPoint(5, "Torus_AP", ap1));

        contents.Add(new AccessPoint(3, "Sphere_AP", ap2)); 


        gameManager = GameManager.Find();
        UI = GameManager.FindUI();

        ChangedInventory();
    }

    private void Update(){ //Update the UI every frame //TODO: Make this a event based thing instead of constant updates
        
    }

    public void ChangedInventory(){
        

        UI.UpdateInventory(contents);
    }
}
