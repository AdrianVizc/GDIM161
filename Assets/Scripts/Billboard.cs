using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (cam != null)
        {
            transform.LookAt(cam.transform);
            transform.Rotate(Vector3.up * 180);
        }        
    }
}
