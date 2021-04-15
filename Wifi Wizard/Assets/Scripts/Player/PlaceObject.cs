using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class PlaceObject : MonoBehaviour
{

    // Init vars
    [SerializeField] private GameObject leftClickInstantiate;
    [SerializeField] private GameObject middleClickInstantiate;
    [SerializeField] private LayerMask ignoreMask;

    private Ray myRay;
    private RaycastHit hit;
    private Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    private Inventory inventory;

    [SerializeField]
    private float maxDistance = 5f;

    private GameManager gameManager;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        gameManager = GameManager.Find();
    }

    void placeFromInventory(int slot, RaycastHit hit)
    {
        AccessPoint ap = inventory.contents[slot];
        print(ap.displayName);
        print(inventory.contents.Count);
        if (ap.amount > 0)
        {
            // Place
            Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            // play place success sound
            AudioManager.instance.PlayOneShot("PlaceAPSuccess");
            // ap.amount--;
        }
        else
        {
            // play place fail sound
            AudioManager.instance.PlayOneShot("PlaceAPFailure");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.GamePaused) return;

        gameManager.UI.ShowTooltip("");
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (Camera.main.transform.forward * maxDistance), Color.red, 0.001f);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, ~ignoreMask)) { // if the ray hits something, store info in this var
            GameObject hitObject = hit.collider.gameObject;
            AccessPoint hitAp = hitObject.GetComponent<AccessPoint>();
            
            //Debug.Log(hitObject);
            //Debug.Log(hitAp);

            if(hitAp != null){
                gameManager.UI.ShowTooltip("'E' to configure AP");
            }else{
                gameManager.UI.ShowTooltip("");
            }

            if (Input.GetMouseButtonDown(0)) { // Left Click

                AccessPoint ap = inventory.contents[0];
                //print(ap.displayName);
                //print(inventory.contents.Count);
                //print("in");

                if (ap.amount > 0)
                {
                    // Place
                    Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    // play place success sound
                    AudioManager.instance.PlayOneShot("PlaceAPSuccess");
                    ap.amount--;
                    gameManager.UI.UpdateInventory(inventory.contents);
                }
                else
                {
                    // play place fail sound
                    AudioManager.instance.PlayOneShot("PlaceAPFailure");
                }




            } 
            else if (Input.GetMouseButtonDown(2)) { // Middle Mouse

                AccessPoint ap = inventory.contents[1];
                if (ap.amount > 0)
                {
                    // Place
                    Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    // play place success sound
                    AudioManager.instance.PlayOneShot("PlaceAPSuccess");
                    ap.amount--;
                    gameManager.UI.UpdateInventory(inventory.contents);
                }
                else
                {
                    // play place fail sound
                    AudioManager.instance.PlayOneShot("PlaceAPFailure");
                }

            }
            else if (Input.GetMouseButtonDown(1))
            { //Right Click


                AccessPoint ap = inventory.contents[2];
                if (ap.amount > 0)
                {
                    // Place
                    Instantiate(ap.prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    // play place success sound
                    AudioManager.instance.PlayOneShot("PlaceAPSuccess");
                    ap.amount--;
                    gameManager.UI.UpdateInventory(inventory.contents);
                }
                else
                {
                    // play place fail sound
                    AudioManager.instance.PlayOneShot("PlaceAPFailure");
                } 


            }

            /*
                            // Destroy if AP
                AccessPoint ap = hit.transform.gameObject.GetComponent<AccessPoint>();
                if (ap != null){
                    Destroy(hit.transform.gameObject);
                    int i = inventory.contents.FindIndex(x => x.displayName == ap.displayName);
                    inventory.contents[i].amount++;
                    gameManager.UI.UpdateInventory(inventory.contents);

                }
                */
        
            if(Input.GetKeyDown(KeyCode.E)){
                
            }
        }
    }

}
