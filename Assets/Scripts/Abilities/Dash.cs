using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : AbilityAbstract
{
    [Header("Dash Multiplier")]
    [SerializeField] private float dash;

    private void Update()
    {
        if (Input.GetKeyDown(GetAc().movementAbility))
        {
            // GetMovement().playerSpeed = dash;
            // Vector3 moveDir = Camera.main.transform.forward * dash;
            // moveDir = new Vector3(moveDir.x, 0, moveDir.z);
            // 
            // GetRb().AddForce(moveDir.normalized * 1000f, ForceMode.Force);
        }
    }


}
