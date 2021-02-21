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

    public delegate void OnCheckBeatsAction();

    public static event OnCheckBeatsAction OnCheckBeats;

    public static event OnCheckBeatsAction OnChange;

    private const float SKIN_WIDTH = 0.1f;
    
    private Collider2D col;
    private Rigidbody2D rb;
    private EntityController cont;
    
    private int numberAbsorbed;
    
    [Range(0.0f,0.99f)]
    [SerializeField] private float memberWeight;

    [SerializeField] private List<AlternationNamePair> alternations;
    private Dictionary<String, AlternatingShooters> getAlternator;

    [SerializeField] private Beatmap beatmapToAdjust;

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
        PruneInactive();
    }

    private void PruneInactive()
    {
        foreach (Transform t in transform)
        {
            if (!t.gameObject.activeInHierarchy)
            {
                Destroy(t.gameObject);
            }
        }
        PruneBadAlternators();
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
        OnChange?.Invoke();
        RaiseChangeEvent();
    }

    private void CheckAlternation()
    {
        PruneBadAlternators();
        StartCoroutine(AdjustMapsAfter(0.1f));
    }

    private void PruneBadAlternators()
    {
        foreach (var ash in getAlternator.Values)
        {
            ash.ShootersToAlternate.RemoveAll(a => a==null);
            ash.ShootersToAlternate.RemoveAll(a => a.transform.parent!=transform);
            ash.ShootersToAlternate.RemoveAll(a => !a.gameObject.activeInHierarchy);
        }
        OnChange?.Invoke();
    }

    public override bool IsAbsorbed() => true;

    private void CalculateAbsorbedUnits()
    {
        numberAbsorbed = GetComponentsInChildren<Absorbable>().Count(k => k.enabled);
    }

    private void AdjustBeatmapLines()
    {
        foreach (var part in getAlternator.Keys)
        {
            BeatmapLine line = beatmapToAdjust.GetPart(part);
            if (line == null) continue;
            if(getAlternator[part].Count == 0)
                line.DeactivateLine();
            else
                line.ActivateLine();
        }
        OnCheckBeats?.Invoke();
    }

    IEnumerator AdjustMapsAfter(float f)
    {
        yield return new WaitForSeconds(f);
        AdjustBeatmapLines();
    }

    public bool HasInstrument(string s)
    {
        AlternatingShooters alternator = getAlternator[s];
        return alternator.Count != 0;
    }
}

[Serializable]
public class AlternationNamePair
{
    public AlternatingShooters alternator;
    public string name;
}
