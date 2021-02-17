using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthHaver))]
public abstract class AbsorbBase : MonoBehaviour
{
    public delegate void Detevent();

    public event Detevent OnDetach;
    public static event Detevent OnChangeAbsorbNumber;
    private HealthHaver hh;

    private int antiStackOverflow;

    public abstract bool IsAbsorbed();
    
    protected virtual void Awake()
    {
        antiStackOverflow = 0;
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

    protected virtual void Update()
    {
        antiStackOverflow = 0;
    }

    protected void RaiseDetachEvent(HealthHaver check)
    {
        if (check == hh && IsAbsorbed())
        {
            OnDetach?.Invoke();
            RaiseChangeEvent();
        }
    }
    
    protected void RaiseDetachEvent()
    {
        antiStackOverflow++;
        if(antiStackOverflow >= 500) return;
        OnDetach?.Invoke();
        RaiseChangeEvent();
    }

    protected void RaiseChangeEvent()
    {
        OnChangeAbsorbNumber?.Invoke();
    }
}
