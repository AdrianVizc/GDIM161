using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public string abilityName;
    [SerializeField] public string abilityDescription;
    private bool isSelected;
    private bool gotOriginalPosition;
    private Vector3 startPosition;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;
    private float tolerance = 0.1f;

    AbilityButtonManager abilityButtonManager;
    AbilityDescriptionHandler abilityDescriptionHandler;

    private void Start()
    {
        isSelected = false;
        gotOriginalPosition = false;

        abilitySlot1 = GameObject.Find("AbilitySlot1");
        abilitySlot2 = GameObject.Find("AbilitySlot2");
        abilitySlot3 = GameObject.Find("AbilitySlot3");       
    }

    public void AbilitySelect()
    {
        abilityButtonManager = FindObjectOfType<AbilityButtonManager>();
        abilityDescriptionHandler = FindObjectOfType<AbilityDescriptionHandler>();

        abilityDescriptionHandler.SetName(abilityName);
        abilityDescriptionHandler.SetDescription(abilityDescription);
        //abilityDescriptionHandler.SetActive(true);

        abilityButtonManager.slotHandler();        

        targetPosition = abilityButtonManager.targetPosition;
        startPosition = transform.position;
        

        if (!gotOriginalPosition)
        {
            originalPosition = startPosition;

            gotOriginalPosition = true;
        }

        if (!isSelected)
        {            
            StartCoroutine(MoveButton());

            isSelected = true;
        }
        else if (isSelected)
        {
            StartCoroutine(ReturnButton());

            isSelected = false;
        }
    }

    private IEnumerator MoveButton()
    {
        float currentTimer = Time.deltaTime;
        float percentDone = 0f;

        if (targetPosition == abilitySlot1.transform.position)
        {
            abilityButtonManager.isAbilitySlot1Empty = true;
        }
        else if (targetPosition == abilitySlot2.transform.position)
        {
            abilityButtonManager.isAbilitySlot2Empty = true;
        }
        else if (targetPosition == abilitySlot3.transform.position)
        {
            abilityButtonManager.isAbilitySlot3Empty = true;
        }


        while (currentTimer < speed)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, percentDone);
            percentDone = currentTimer / speed;
            currentTimer += Time.deltaTime;

            yield return null;
        }
        transform.position = Vector2.Lerp(startPosition, targetPosition, 1);
        abilityButtonManager.ClickedAbilitySlot(false);
    }

    private IEnumerator ReturnButton()
    {
        float currentTimer = Time.deltaTime;
        float percentDone = 0f;

        if (Vector3.Distance(transform.position, abilitySlot1.transform.position) < tolerance)
        {
            abilityButtonManager.isAbilitySlot1Empty = false;
        }
        else if (Vector3.Distance(transform.position, abilitySlot2.transform.position) < tolerance)
        {
            abilityButtonManager.isAbilitySlot2Empty = false;
        }
        else if (Vector3.Distance(transform.position, abilitySlot3.transform.position) < tolerance)
        {
            abilityButtonManager.isAbilitySlot3Empty = false;
        }

        while (currentTimer < speed)
        {
            transform.position = Vector2.Lerp(startPosition, originalPosition, percentDone);
            percentDone = currentTimer / speed;
            currentTimer += Time.deltaTime;

            yield return null;
        }
        transform.position = Vector2.Lerp(startPosition, originalPosition, 1);
    }
}
