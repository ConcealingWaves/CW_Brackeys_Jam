using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Absorber : AbsorbBase
{
    public delegate void AbsorbAction(Absorber a, Absorbable ab);
    public static event AbsorbAction OnAbsorb;

    private const float SKIN_WIDTH = 0.1f;
    
    private List<Absorbable> absorbedUnits;
    private Collider2D col;
    private Rigidbody2D rb;

    public Rigidbody2D Rb
    {
        get => rb;
        private set => rb = value;
    }

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider2D>();
        absorbedUnits = new List<Absorbable>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Absorbable absorbable = other.gameObject.GetComponent<Absorbable>();
        if (absorbable != null && absorbable.enabled)
        {
            AddToAbsorbedUnits(absorbable);
        }
    }

    public void AddToAbsorbedUnits(Absorbable a)
    {
        a.transform.parent = transform;
        a.GetAbsorbed(this);
        absorbedUnits.Add(a);
        OnAbsorb?.Invoke(this, a);
    }

    public void RemoveAbsorbedUnitFromList(Absorbable a)
    {
        absorbedUnits.Remove(a);
    }

    public override bool IsAbsorbed() => true;

}
