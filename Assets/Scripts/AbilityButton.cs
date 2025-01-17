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
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private GameObject childObject;

    private bool isSelected;
    private Vector3 startPosition;

    private void Start()
    {
        if (!isButtonActive)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        isSelected = false;

        startPosition = transform.localPosition;
    }

    public void AbilitySelect()
    {
        if (!isSelected)
        {


            if (childObject != null)
            {
                childObject.transform.SetParent(null);
                Debug.Log(childObject.name + " has been removed from its parent.");
            }

            StartCoroutine(MoveButton());
            
            isSelected = true;
        }
        else if (isSelected)
        {
            //transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
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
            Debug.Log(percentDone);
            currentTimer += Time.deltaTime;
            yield return null;
        }
        
    }
}
