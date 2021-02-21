using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InstrumentIndicators : MonoBehaviour
{
    [SerializeField] private Color unhighlightedColor;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private List<InstrumentIndicator> instrumentIndicators;

    [SerializeField] private Absorber absorberToWatch;

    public bool Completed;
    public int MaxInstruments;

    private void OnEnable()
    {
        Absorber.OnChange += UpdateIndicators;
    }
    
    private void OnDisable()
    {
        Absorber.OnChange += UpdateIndicators;
    }

    private void Start()
    {
        UpdateIndicators();
    }

    private void UpdateIndicators()
    {
        List<bool> hasInstrument = new List<bool>();
        foreach (var indicator in instrumentIndicators)
        {
            bool b = absorberToWatch.HasInstrument(indicator.Instrument);
            hasInstrument.Add(b);
            indicator.TextColorTo(b
                ? highlightedColor
                : unhighlightedColor);
        }

        MaxInstruments = Mathf.Max(MaxInstruments, hasInstrument.Where(s=>s).ToList().Count);
        Completed = Completed || hasInstrument.Aggregate((a, b) => a && b);
        if (Completed)
            GlobalStats.instance.EverCompleted = Completed;
    }
}
