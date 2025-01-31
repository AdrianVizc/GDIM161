using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseScale : MonoBehaviour
{
    [SerializeField] private Vector3 scale = new Vector3(1.2f, 1.2f);

    public void PointerEnter()
    {
        transform.localScale = scale;
    }

    public void PointerExit()
    {
        transform.localScale = new Vector3(1f, 1f);

    }
}
