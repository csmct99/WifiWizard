using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnectivity : MonoBehaviour {

    private List<AccessPoint> connectedAPs;

    private UIController ui;
    public int connectedAPCount = 0;

    // Start is called before the first frame update
    void Start() {
        connectedAPs = new List<AccessPoint>();
        ui = GameManager.FindUI();
        ui.SubscribeConnectedPoint();
    }

    private void OnTriggerEnter(Collider c) {
        AccessPoint ap;
        Transform parent = c.gameObject.transform.parent;
        Debug.Log("Triger");
        if(parent != null){
            Debug.Log("Parent found");
            if (ap = parent.gameObject.GetComponent<AccessPoint>())
            {
                connectedAPs.Add(ap);
                connectedAPCount++;
                ap.EnteredDetector(this);
                Debug.Log("Access point connected(" + connectedAPs.Count + ":" + connectedAPCount + "): " + c.gameObject.name);
                ui.ChangeConnectedPoints( ((connectedAPCount == 1) ? +1 : 0) ); //Add an ap counter if this is the first ap added
                Debug.Log((connectedAPCount == 1) ? +1 : 0);
                
            }
        }
    }

    private void OnTriggerExit(Collider c) {
        AccessPoint ap;
        Transform parent = c.gameObject.transform.parent;
        if (parent != null) {
            if (ap = parent.gameObject.GetComponent<AccessPoint>()) {
                connectedAPs.Remove(ap);
                connectedAPCount--;
                ap.LeftDetector(this);
                Debug.Log("Access point disconnected(" + connectedAPs.Count + ":" + connectedAPCount + "): " + ap.gameObject.name);
                ui.ChangeConnectedPoints(((connectedAPCount == 0) ? -1 : 0)); //Remove a ap counter if this was the last connect ap
            }
        }
    }

    public void Unsubscribe(AccessPoint ap){
        connectedAPs.Remove(ap);
        connectedAPCount--;
        Debug.Log("Access point disconnected(" + connectedAPs.Count + ":"+connectedAPCount +"): " + ap.gameObject.name);
        ui.ChangeConnectedPoints( ((connectedAPCount == 0) ? -1 : 0 ) ); //Remove a ap counter if this was the last connect ap
        
    }

}
