using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    public delegate void EndAction(Beatmap hoc);

    public static event EndAction OnEnd;
    
    [SerializeField] private float bpm;
    [SerializeField] private List<InstrumentLinePair> beatmapLines;
    [Space(5)] 
    [SerializeField] private bool playOnStart;
    private Dictionary<string, BeatmapLine> parts;

    private bool ended;
    private void Awake()
    {
        ended = false;
        parts = new Dictionary<string, BeatmapLine>();
        foreach (var ilp in beatmapLines)
        {
            parts.Add(ilp.PartName, ilp.BeatmapLine);
            ilp.BeatmapLine.LineName = ilp.PartName;
            ilp.BeatmapLine.Bpm = bpm;
        }
    }

    private void OnEnable()
    {
        BeatmapLine.End += RaiseEndAction;
    }
    
    private void OnDisable()
    {
        BeatmapLine.End -= RaiseEndAction;
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartRhyhthm();
        }
    }

    public BeatmapLine GetPart(string s)
    {
        if (parts.ContainsKey(s)) 
            return parts[s]; 
        //Debug.LogWarning($"I have no part named {s}, null value returned.");
        return null;
    }

    public void StartRhyhthm()
    {
        foreach (var line in parts.Values)
        {
            line.StartRhythm();
        }
    }

    private void RaiseEndAction(string lineEnded)
    {
        OnEnd?.Invoke(this);
    }
    
}

[System.Serializable]
public class InstrumentLinePair
{
    public string PartName;
    public BeatmapLine BeatmapLine;
}
