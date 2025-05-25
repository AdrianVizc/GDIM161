using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public bool isAbilitySlot1Full = false;
    [HideInInspector] public bool isAbilitySlot2Full = false;
    [HideInInspector] public bool isAbilitySlot3Full = false;
    [HideInInspector] public bool allSlotsFull = false;
    [HideInInspector] public bool hasClickedSlot = false;

    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;


    public string slot1 = "Slot 1: ";
    public string slot2 = "Slot 2: ";
    public string slot3 = "Slot 3: ";

    private void Start()
    {
        abilitySlot1 = GameObject.Find("AbilitySlot1");
        abilitySlot2 = GameObject.Find("AbilitySlot2");
        abilitySlot3 = GameObject.Find("AbilitySlot3");
    }

    private void Update()
    {
        if (isAbilitySlot1Full && isAbilitySlot2Full && isAbilitySlot3Full)
        {
            allSlotsFull = true;
        }
        else
        {
            allSlotsFull = false;
        }
    }

    public void slotHandler()
    {
        if (hasClickedSlot)
        {
            return;
        }
        else if (!isAbilitySlot1Full)
        {
            targetPosition = abilitySlot1.transform.position;
        }
        else if (!isAbilitySlot2Full)
        {
            targetPosition = abilitySlot2.transform.position;
        }
        else if (!isAbilitySlot3Full)
        {
            targetPosition = abilitySlot3.transform.position;
        }
    }

    public void SetAbilitySelectPosition(Vector3 position)
    {
        targetPosition = position;       
    }

    public void ClickedAbilitySlot(bool x)
    {
        hasClickedSlot = x;
    }    
}
