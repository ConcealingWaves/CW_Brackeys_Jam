using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReader : ScriptableObject
{
    private bool thrustEngaged;
    private float rotationInput;

    public virtual void Enter(EntityController cont)
    {
    }

    public virtual void Init(EntityController cont)
    {
    }
    
    public virtual void Exit(EntityController cont)
    {
    }

    public abstract void Tick(EntityController cont);

    public bool ThrustEngaged
    {
        get => thrustEngaged;
        protected set => thrustEngaged = value;
    }

    public float RotationInput
    {
        get => rotationInput;
        protected set => rotationInput = value;
    }

    protected void InvokeShoot(EntityController cont)
    {
        if(cont!=null)
            cont.ShootAction();
    }
}

