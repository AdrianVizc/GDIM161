using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GnomeRotationHandler : MonoBehaviour
{
    private GameObject capsule;
    private Camera mainCamera;
    private void Start()
    {
        capsule = this.gameObject;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        capsule.transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.y * 2f, 0f);
    }
}
