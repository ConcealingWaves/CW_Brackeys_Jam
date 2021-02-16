using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Absorber : MonoBehaviour
{
    private List<Absorbable> absorbedUnits;
    private Collider2D col;
    private Rigidbody2D rb;

    public Rigidbody2D Rb
    {
        get => rb;
        private set => rb = value;
    }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        absorbedUnits = new List<Absorbable>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Absorbable absorbable = other.gameObject.GetComponent<Absorbable>();
        if (absorbable != null)
        {
            AddToAbsorbedUnits(absorbable);
        }
    }

    public void AddToAbsorbedUnits(Absorbable a)
    {
        a.transform.parent = transform;
        a.GetAbsorbed(this);
        absorbedUnits.Add(a);
    }
}
