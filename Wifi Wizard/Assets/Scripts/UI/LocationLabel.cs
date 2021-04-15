using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationLabel : MonoBehaviour
{
    private TextMeshProUGUI locationLabel;
    private UIController ui;

    private void Start() {
        ui = GameManager.FindUI();
    }


    /*
     * Change the HUD label upon collision
     */
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        //Debug.Log("Player moved to :" + gameObject.name);
        ui.ChangeLocation(gameObject.name);
    }
}
