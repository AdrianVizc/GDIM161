using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJumpUI : MonoBehaviour
{
    private Sprite image;
    private GameObject key;
    private GameObject spacebar;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Key")
            {
                key = child.gameObject;
            }
            if (child.name == "SpacebarUI")
            {
                spacebar = child.gameObject;
            }
        }
        if (GetComponentInParent<Image>().sprite.name == "DoubleJump_0")
        {
            key.SetActive(false);
            spacebar.SetActive(true);
        }
        else
        {
            key.SetActive(true);
            spacebar.SetActive(false);
        }
    }
}
