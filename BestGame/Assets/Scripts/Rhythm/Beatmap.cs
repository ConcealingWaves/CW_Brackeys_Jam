using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap : MonoBehaviour
{
    [SerializeField] private List<InstrumentLinePair> beatmapLines;
    private Dictionary<string, BeatmapLine> parts;

    private void Awake()
    {
        parts = new Dictionary<string, BeatmapLine>();
        foreach (var ilp in beatmapLines)
        {
            parts.Add(ilp.PartName, ilp.BeatmapLine);
        }
    }

    public BeatmapLine GetPart(string s)
    {
        if (parts.ContainsKey(s)) 
            return parts[s]; 
        Debug.LogWarning($"I have no part named {s}, null value returned.");
        return null;
    }
}

[System.Serializable]
public class InstrumentLinePair
{
    public string PartName;
    public BeatmapLine BeatmapLine;
}
