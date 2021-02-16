using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EntityController))]
public class Absorber : AbsorbBase
{
    public delegate void AbsorbAction(Absorber a, Absorbable ab);
    public static event AbsorbAction OnAbsorb;

    private const float SKIN_WIDTH = 0.1f;
    
    private Collider2D col;
    private Rigidbody2D rb;
    private EntityController cont;
    
    private int numberAbsorbed;
    
    [Range(0.0f,0.99f)]
    [SerializeField] private float memberWeight;

    public int NumberAbsorbed
    {
        get => Mathf.Max(0, numberAbsorbed);
    }

    public Rigidbody2D Rb
    {
        get => rb;
        private set => rb = value;
    }

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider2D>();
        cont = GetComponent<EntityController>();
        numberAbsorbed = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnChangeAbsorbNumber += CalculateAbsorbedUnits;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnChangeAbsorbNumber -= CalculateAbsorbedUnits;
    }

    private void Update()
    {
        cont.MoveSpeedFactor = Mathf.Pow(1-memberWeight , numberAbsorbed);
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
        OnAbsorb?.Invoke(this, a);
        RaiseChangeEvent();
    }
    

    public override bool IsAbsorbed() => true;

    private void CalculateAbsorbedUnits()
    {
        numberAbsorbed = GetComponentsInChildren<Absorbable>().Count(k => k.enabled);
    }
}
