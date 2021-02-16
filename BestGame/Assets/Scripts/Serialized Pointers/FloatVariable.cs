using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void ChangeAction(float to);

    public event ChangeAction OnValueChangedTo;
    
    public float initialValue;

    private float value;

    public float Value
    {
        get => value;
        set { this.value = value; OnValueChangedTo?.Invoke(value); }
    }


    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }
}
