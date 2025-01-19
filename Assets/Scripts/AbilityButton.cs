using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBoxHider : MonoBehaviour
{
    [SerializeField] private bool isButtonActive;
    [Space]
    [SerializeField] private float speed;
        
    private bool isSelected;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private GameObject abilitySlot1;
    private GameObject abilitySlot2;
    private GameObject abilitySlot3;

    private void Start()
    {
        if (!isButtonActive)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponent<Button>().enabled = false; //removes image and button but keeps gameobject so grid layout keeps spacing
        }

        abilitySlot1 = GameObject.Find("AbilitySlot1");
        abilitySlot2 = GameObject.Find("AbilitySlot2");
        abilitySlot3 = GameObject.Find("AbilitySlot3");

        isSelected = false;    
    }

    public void AbilitySelect()
    {
        targetPosition = abilitySlot1.transform.position;
        startPosition = transform.position;
        Debug.Log("start pos " + startPosition);
        Debug.Log("target pos " + targetPosition);


        if (!isSelected)
        {         
            StartCoroutine(MoveButton());
            
            isSelected = true;
        }
        else if (isSelected)
        {
            
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

    }
}
