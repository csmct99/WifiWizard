using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnectivity : MonoBehaviour
{
    public Material noConnection;
    public Material connection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wifi in GameObject.FindGameObjectsWithTag("Wifi"))
        {
            Collider c = wifi.GetComponent<Collider>();
            Vector3 closest = c.ClosestPoint(transform.position);
            if ((closest - transform.position).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon)
            {
                gameObject.GetComponent<Renderer>().material = connection;
                return;
            }
        }
        gameObject.GetComponent<Renderer>().material = noConnection;
    }
}
