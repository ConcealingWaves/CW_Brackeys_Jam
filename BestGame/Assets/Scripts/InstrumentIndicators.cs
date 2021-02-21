using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstrumentIndicators : MonoBehaviour
{
    [SerializeField] private Color unhighlightedColor;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private List<InstrumentIndicator> instrumentIndicators;

    [SerializeField] private Absorber absorberToWatch;

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
        foreach (var indicator in instrumentIndicators)
        {
            indicator.TextColorTo(absorberToWatch.HasInstrument(indicator.Instrument)
                ? highlightedColor
                : unhighlightedColor);
        }
    }
}
