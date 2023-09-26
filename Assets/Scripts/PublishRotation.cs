using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublishRotation : MonoBehaviour
{
    private GameObject centerEye;

    private Vector3 headsetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        centerEye = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        headsetPos = centerEye.transform.position;
        //Debug.Log(headsetPos);
    }
}
