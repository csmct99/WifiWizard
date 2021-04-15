using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPoint : MonoBehaviour {
        public int amount;
        public string displayName;
        public GameObject prefab;

        private List<CheckConnectivity> detectors;

        public AccessPoint(int amount, string displayName, GameObject prefab){
            this.amount = amount;
            this.displayName = displayName;
            this.prefab = prefab;
        }

        private void Start(){
            detectors = new List<CheckConnectivity>();
        }

        public void EnteredDetector(CheckConnectivity detector) {
            detectors.Add(detector);
        }

        public void LeftDetector(CheckConnectivity detector) {
            detectors.Remove(detector);
        }



        private void OnDestroy(){
            foreach(CheckConnectivity detector in detectors){
                detector.Unsubscribe(this);
            }
        }
}
