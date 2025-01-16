using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBoxHider : MonoBehaviour
{
    [SerializeField] private bool isButtonActive;

    private void Start()
    {
        if (!isButtonActive)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }
}
