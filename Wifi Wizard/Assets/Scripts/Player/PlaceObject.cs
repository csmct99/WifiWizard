using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class PlaceObject : MonoBehaviour {
    
    // Init vars
    [SerializeField] private GameObject leftClickInstantiate;
    [SerializeField] private GameObject middleClickInstantiate;
    [SerializeField] private LayerMask ignoreMask;

    private Ray myRay;
    private RaycastHit hit;
    private Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    private Inventory inventory;

    void Start(){
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void LateUpdate() {
            RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~ignoreMask)) { // if the ray hits something, store info in this var
            if (Input.GetMouseButtonDown(0)){ // Left Click
                
                AccessPoint ap = inventory.contents[0];
                print(ap.displayName);
                print(inventory.contents.Count);
                print("in");
                    if(ap.amount > 0){
                        // Place
                        Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        ap.amount --;
                    }
                

            } if (Input.GetMouseButtonDown(2)) { // Middle Mouse
                
                AccessPoint ap = inventory.contents[1];
                    if(ap.amount > 0){
                        // Place
                        Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        ap.amount --;
                    }

            } else if (Input.GetMouseButtonDown(1)) { //Right Click

                // Destroy if same tag
                if (hit.transform.gameObject.tag == leftClickInstantiate.tag || hit.transform.gameObject.tag == leftClickInstantiate.tag)
                    Destroy(hit.transform.gameObject);
                

            }
        }
    }
}
