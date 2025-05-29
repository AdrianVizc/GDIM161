using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCD : MonoBehaviour
{
    [SerializeField] private AbilityHolder holder;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(holder.ability.name);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(holder.GetCurrentCD());
    }
}
