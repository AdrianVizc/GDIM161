using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public bool isAbilitySlot1Empty = false;
    [HideInInspector] public bool isAbilitySlot2Empty = false;
    [HideInInspector] public bool isAbilitySlot3Empty = false;
    [HideInInspector] public bool hasClickedSlot = false;
    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;

    private void Start()
    {
        abilitySlot1 = GameObject.Find("AbilitySlot1");
        abilitySlot2 = GameObject.Find("AbilitySlot2");
        abilitySlot3 = GameObject.Find("AbilitySlot3");
    }

    public void slotHandler()
    {
        if (hasClickedSlot)
        {
            return;
        }
        else if (!isAbilitySlot1Empty)
        {
            targetPosition = abilitySlot1.transform.position;
        }
        else if (!isAbilitySlot2Empty)
        {
            targetPosition = abilitySlot2.transform.position;
        }
        else if (!isAbilitySlot3Empty)
        {
            targetPosition = abilitySlot3.transform.position;
        }
    }

    public void SetAbilitySelectPosition(Vector3 position)
    {
        targetPosition = position;
        Debug.Log(targetPosition);        
    }

    public void ClickedAbilitySlot(bool x)
    {
        hasClickedSlot = x;
    }
}
