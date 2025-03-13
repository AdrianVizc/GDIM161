using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomNameErrorText : MonoBehaviour
{
    [SerializeField] private float fadeTime = 5;
    [SerializeField] private TMP_Text roomNameErrorTextText;
    [SerializeField] private GameObject roomNameErrorTextObject;    

    private void Start()
    {
    }

    private void OnEnable()
    {
        roomNameErrorTextText.color = new Color(roomNameErrorTextText.color.r, roomNameErrorTextText.color.g, roomNameErrorTextText.color.b, 1f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color textColor = roomNameErrorTextText.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            roomNameErrorTextText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }

        roomNameErrorTextText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        roomNameErrorTextObject.SetActive(false);
    }
}
