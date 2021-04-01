using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationLabel : MonoBehaviour
{
    private TextMeshProUGUI locationLabel;

    private void Start()
    {
        Debug.Log("Start");
        locationLabel = GameObject.Find("LocationLabel").GetComponent<TextMeshProUGUI>();
        Debug.Log("Start locationLabel = " + locationLabel);
    }


    /*
     * Change the HUD label upon collision
     */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        locationLabel.text = other.name;
    }
}
