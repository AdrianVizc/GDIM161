using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityDescriptionHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityDescription;

    public void SetName(string text)
    {
        abilityName.text = text;
    }

    public void SetDescription(string text)
    {
        abilityDescription.text = text;
    }

    public void SetActive(bool b)
    {
        if (b)
        {
            return; 
        }
        else
        {
            abilityName.text = "";
            abilityDescription.text = "";
        }
    }
}
