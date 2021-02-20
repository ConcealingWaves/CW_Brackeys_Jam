using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsator : MonoBehaviour //we have like less than 24 hours left in this game jam clean code prarctices be damned
{
    private Beatmap mapToRead;
    [Header("Pulsation")]
    [SerializeField] private float pulsateTo;
    [SerializeField] private float pulsateCycleBeats;
    private float pulsateCycleSeconds;
    private Vector3 originalScale;
    private Vector3 pulsateToScale;
    
    private void Awake()
    {
        originalScale = transform.localScale;
        pulsateToScale = originalScale * pulsateTo;
    }

    private void Start()
    {
        mapToRead = FindObjectOfType<Beatmap>();
        pulsateCycleSeconds = MusicUtility.BeatsToSeconds(pulsateCycleBeats, mapToRead.Bpm);
    }

    private void Update()
    {
        Pulsate();
    }

    private void Pulsate()
    {
        float timeInCycle = (mapToRead.TimeSinceStart % pulsateCycleSeconds)/pulsateCycleSeconds;
        float sinterp = Mathf.Cos(timeInCycle * 2 * Mathf.PI) / 2 + 0.5f;
        Vector3 toScale = Vector3.Lerp(originalScale,pulsateToScale,sinterp);
        transform.localScale = toScale;
    }
}
