using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessPoint : MonoBehaviour {
        public int amount;
        public string displayName;
        public GameObject prefab;

        public AccessPoint(int amount, string displayName, GameObject prefab){
            this.amount = amount;
            this.displayName = displayName;
            this.prefab = prefab;
        }
}
