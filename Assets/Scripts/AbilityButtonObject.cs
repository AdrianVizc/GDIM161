using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonObject : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isSelected;
    private bool gotOriginalPosition;
    private Vector3 startPosition;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private GameObject abilitySlot;

    AbilityButtonManager abilityButtonManager;

    private void Start()
    {
        isSelected = false;
        gotOriginalPosition = false;
    }

    public void AbilitySelect()
    {
        abilityButtonManager = FindObjectOfType<AbilityButtonManager>();

        /*if (abilityButtonManager == null )
        {
            abilitySlot = GameObject.Find("AbilitySlot1");
            targetPosition = abilitySlot.transform.position;
        }
        else
        {
            targetPosition = abilityButtonManager.targetPosition;
        }*/
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


        while (currentTimer < speed)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, percentDone);
            percentDone = currentTimer / speed;
            currentTimer += Time.deltaTime;

            yield return null;
        }

        transform.position = Vector2.Lerp(startPosition, targetPosition, 1);
        Debug.Log(targetPosition);
    }

    private IEnumerator ReturnButton()
    {
        float currentTimer = Time.deltaTime;
        float percentDone = 0f;


        while (currentTimer < speed)
        {
            transform.position = Vector2.Lerp(targetPosition, originalPosition, percentDone);
            percentDone = currentTimer / speed;
            currentTimer += Time.deltaTime;

            yield return null;
        }

        transform.position = Vector2.Lerp(targetPosition, originalPosition, 1);
    }
}
