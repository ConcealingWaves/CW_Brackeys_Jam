using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Beatmap : MonoBehaviour
{
    public delegate void EndAction(Beatmap hoc);

    public static event EndAction OnEnd;
    
    [SerializeField] private float bpm;
    [SerializeField] private List<InstrumentLinePair> beatmapLines;
    [Space(5)] 
    [SerializeField] private bool playOnStart;
    private Dictionary<string, BeatmapLine> parts;

    [SerializeField] private AudioSource ambienceSource;
    private IEnumerator currentFadeAction;

    public float TimeSinceStart => beatmapLines[0].BeatmapLine.TimeInto;

    private bool ended;
    private bool ambienceOn;

    public float Bpm => bpm;
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

        ambienceOn = true;
    }

    private void OnEnable()
    {
        BeatmapLine.End += RaiseEndAction;
        Absorber.OnCheckBeats += CheckAmbience;
    }
    
    private void OnDisable()
    {
        BeatmapLine.End -= RaiseEndAction;
        Absorber.OnCheckBeats -= CheckAmbience;
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

    private bool AnyPlaying()
    {
        foreach (var line in parts.Values)
        {
            if (line.IsPlaying)
            {
                print(line.LineName);
                return true;
            }
        }

        return false;
    }

    private void CheckAmbience()
    {
        if(currentFadeAction !=null) StopCoroutine(currentFadeAction);
        if (AnyPlaying() && ambienceOn)
        {
            currentFadeAction = FadeAmbience(1,0);
            StartCoroutine(currentFadeAction);
        }
        else if (!AnyPlaying() && !ambienceOn)
        {
            currentFadeAction = FadeAmbience(0, 1);
            StartCoroutine(currentFadeAction);
        }
    }

    IEnumerator FadeAmbience(float from, float to)
    {
        ambienceOn = to > from;
        float start = Time.time;
        float fadeTime = MusicUtility.BeatsToSeconds(4, Bpm);
        while (Time.time - start <= fadeTime)
        {
            ambienceSource.volume = Mathf.Lerp(from,to,(Time.time-start)/fadeTime);
            yield return null;
        }
        ambienceSource.volume = to;
    }
    
    
}

[System.Serializable]
public class InstrumentLinePair
{
    public string PartName;
    public BeatmapLine BeatmapLine;
}
