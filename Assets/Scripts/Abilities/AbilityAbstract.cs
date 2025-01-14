using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityAbstract : MonoBehaviour
{
    private Rigidbody rb;
    private Movement pm;
    private AbilityControlHandler ac;

    private PickUp item;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>();
        ac = GetComponent<AbilityControlHandler>();
    }

    public Rigidbody GetRb()
    {
        return rb;
    }

    public Movement GetMovement()
    {
        return pm;
    }

    public AbilityControlHandler GetAc()
    {
        return ac;
    }

    public abstract void Apply(Movement player);
}
