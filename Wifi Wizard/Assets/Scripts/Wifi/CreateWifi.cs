using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWifi : MonoBehaviour
{
    [SerializeField] private GameObject wifiShape;
    [SerializeField] private int sizeY;
    [SerializeField] private int sizeXZ;
    [SerializeField] private int offset;
    private GameObject area;

    void Awake()
    {
        area = Instantiate(wifiShape, transform.position + (transform.up*offset), transform.rotation * wifiShape.transform.rotation);
        area.transform.localScale = Vector3.Scale(area.transform.localScale, new Vector3(sizeXZ, sizeXZ, sizeY));
        area.transform.SetParent(gameObject.transform);
    }

    void OnDestroy() {
        //Destroy(area);
    }
}
