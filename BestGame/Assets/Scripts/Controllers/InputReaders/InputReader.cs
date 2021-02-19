using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReader : ScriptableObject
{
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
    

    protected void InvokeShoot(EntityController cont)
    {
        if(cont!=null)
            cont.ShootAction();
    }
}

