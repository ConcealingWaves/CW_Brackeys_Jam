using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthHaver))]
public abstract class AbsorbBase : MonoBehaviour
{
    public delegate void Detevent();

    public event Detevent OnDetach;
    private HealthHaver hh;

    public abstract bool IsAbsorbed();
    
    protected virtual void Awake()
    {
        hh = GetComponent<HealthHaver>();
    }

    protected virtual void OnEnable()
    {
        HealthHaver.OnDie += RaiseDetachEvent;
    }

    protected virtual void OnDisable()
    {
        HealthHaver.OnDie -= RaiseDetachEvent;
    }

    protected void RaiseDetachEvent(HealthHaver check)
    {

        if (check == hh && IsAbsorbed())
        {
            print("s");
            OnDetach?.Invoke();
        }
    }
    
    protected void RaiseDetachEvent()
    {
        print("f");
        OnDetach?.Invoke();
    }
}
