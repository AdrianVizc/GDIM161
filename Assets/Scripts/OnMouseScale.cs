using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnMouseScale : MonoBehaviour
{
    [SerializeField] private Vector3 scale = new Vector3(1.2f, 1.2f);
    [SerializeField] private GameObject button;
    
    AbilityDescriptionHandler abilityDescriptionHandler;

    public void PointerEnter()
    {
        transform.localScale = scale;

        //abilityButtonObject = FindObjectOfType<AbilityButtonObject>();
        abilityDescriptionHandler = FindObjectOfType<AbilityDescriptionHandler>();
        abilityDescriptionHandler.SetName(button.GetComponent<AbilityButtonObject>().abilityName);
        abilityDescriptionHandler.SetDescription(button.GetComponent<AbilityButtonObject>().abilityDescription);
        //abilityDescriptionHandler.SetActive(true);
    }

    public void PointerExit()
    {
        transform.localScale = new Vector3(1f, 1f);

    }
}
