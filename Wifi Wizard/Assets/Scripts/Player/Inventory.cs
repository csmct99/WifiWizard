using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour {

    [SerializeField] private GameObject ap1;
    [SerializeField] private GameObject ap2;

    [SerializeField] private GameObject canvas;

    public List<AccessPoint> contents = new List<AccessPoint>(); //Make empty inventory contents.

    private void Start(){
        contents.Add(new AccessPoint(5, "Torus_AP", ap1));

        contents.Add(new AccessPoint(3, "Sphere_AP", ap2)); 
    }

    private void Update(){ //Update the UI every frame //TODO: Make this a event based thing instead of constant updates
        if(canvas != null){
            
            Transform inventorySlotsUI = canvas.transform.Find("Inventory").Find("SlotsLayout");
            Transform[] slots = inventorySlotsUI.GetComponentsInChildren<Transform>();

            int n = 0; //This is a hardcoded prototype only way of doing this. Ill change it later
            foreach(Transform t in slots){
                if(t.CompareTag("InventorySlot")){
                    Transform ui = t.Find("Icon");
                    ui.Find("Name").gameObject.GetComponent<TMP_Text>().text = contents[n].displayName;
                    ui.Find("AmountLeft").gameObject.GetComponent<TMP_Text>().text = contents[n].amount.ToString();
                    n++; //This is a hardcoded prototype only way of doing this. Ill change it later
                }
            }

        }
    }
}
