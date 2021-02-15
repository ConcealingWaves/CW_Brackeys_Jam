using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReader : ScriptableObject
{
    public event Action OnShoot; 
    
    private bool thrustEngaged;
    private float rotationInput;

    public virtual void Enter()
    {
    }
    
    public virtual void Exit()
    {
    }

    public abstract void Tick();

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

    protected void InvokeShoot()
    {
        OnShoot?.Invoke();
    }
}

