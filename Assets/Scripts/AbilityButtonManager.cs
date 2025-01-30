using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour
{
    public Vector3 targetPosition;

    public void SetAbilitySelectPosition(Vector3 position)
    {
        targetPosition = position;
        Debug.Log(targetPosition);
    }
}
