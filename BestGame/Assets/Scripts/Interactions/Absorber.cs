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

    [SerializeField] private List<AlternationNamePair> alternations;
    private Dictionary<String, AlternatingShooters> getAlternator;

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

        getAlternator = new Dictionary<string, AlternatingShooters>();
        foreach (var anp in alternations)
        {
            getAlternator.Add(anp.name, anp.alternator);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnChangeAbsorbNumber += CalculateAbsorbedUnits;
        OnChangeAbsorbNumber += CheckAlternation;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnChangeAbsorbNumber -= CalculateAbsorbedUnits;
        OnChangeAbsorbNumber -= CheckAlternation;
    }

    protected override void Update()
    {
        base.Update();
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
        getAlternator[a.GetRhythmPart()].AddShooter(a.cont);
        OnAbsorb?.Invoke(this, a);
        RaiseChangeEvent();
    }

    private void CheckAlternation()
    {
        foreach (var ash in getAlternator.Values)
        {
            ash.ShootersToAlternate.RemoveAll(a => a==null);
            ash.ShootersToAlternate.RemoveAll(a => a.transform.parent!=transform);
        }
    }

    public override bool IsAbsorbed() => true;

    private void CalculateAbsorbedUnits()
    {
        numberAbsorbed = GetComponentsInChildren<Absorbable>().Count(k => k.enabled);
    }
}

[Serializable]
public class AlternationNamePair
{
    public AlternatingShooters alternator;
    public string name;
}
