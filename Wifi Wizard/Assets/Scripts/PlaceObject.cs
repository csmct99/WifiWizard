using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    // Init vars
    Ray myRay;
    RaycastHit hit;
    public GameObject objectToInstantiate;
    Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
  
    // Update is called once per frame
    void Update()
    {
        myRay = Camera.main.ScreenPointToRay(screenCenter); // ray will go center of main camera to mouse direction
        if (Physics.Raycast(myRay, out hit)) // if the ray hits something, store info in this var
        {
            if (Input.GetMouseButtonDown(0)) // what to do when mouse button pressed
            {
                // Place
                GameObject placed = Instantiate(objectToInstantiate, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // Destroy if same tag
                if (hit.transform.gameObject.tag == objectToInstantiate.tag)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
