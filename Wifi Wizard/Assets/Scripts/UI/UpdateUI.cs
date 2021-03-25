using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public int neededPercent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int total = 0;
        int connected = 0;
        foreach (GameObject point in GameObject.FindGameObjectsWithTag("Point"))
        {
            total += 1;
            connected += point.GetComponent<CheckConnectivity>().isConnected() ? 1 : 0;
        }

        int currentPercent = (int)(connected / (double)total * 100);
        gameObject.GetComponent<Text>().color = currentPercent >= neededPercent ? Color.green : Color.black;
        gameObject.GetComponent<Text>().text = $"{currentPercent}/{neededPercent}%";
    }
}
