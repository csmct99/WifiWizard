using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWifi : MonoBehaviour
{
    public GameObject wifiShape;
    public int sizeY;
    public int sizeXZ;
    public int offset;
    GameObject area;

    void Awake()
    {
        area = Instantiate(wifiShape, transform.position + (transform.up*offset), Quaternion.FromToRotation(transform.forward, transform.up));
        area.transform.localScale = Vector3.Scale(area.transform.localScale, new Vector3(sizeXZ, sizeY, sizeXZ));
    }

    void OnDestroy()
    {
        Destroy(area);
    }
}
