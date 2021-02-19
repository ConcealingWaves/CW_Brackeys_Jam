using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Command n sets next tick n beats ahead; command -1 stops the rhythm.

[RequireComponent(typeof(AudioSource))]
public class BeatmapLine : MonoBehaviour
{
    public delegate void TickAction(string line);

    public static event TickAction Tick;
    public static event TickAction End;

    public const float NEGLIGIBLE_VOLUME = 0.01f;

    private string line;
    private List<float> commands;
    
    private string lineName;
    private AudioSource source;

    private float initialVolume;
    
    private bool isInRhythm;

    private float bpm;

    private float lastTick;
    private float nextTick;
    private int currentCommandIndex;

    [SerializeField] private AudioClip clip;
    [SerializeField] private TextAsset commandsFile;

    public string Line
    {
        get => line;
        set => line = value;
    }
    
    public string LineName
    {
        get => lineName;
        set => lineName = value;
    }

    public float Bpm
    {
        get => bpm;
        set => bpm = value;
    }

    public bool IsPlaying
    {
        get => source.volume < NEGLIGIBLE_VOLUME;
    }
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        initialVolume = source.volume;
        source.Stop();
        source.playOnAwake = false;
        
        line = CommandsReader.Read(commandsFile);
        commands = LineStringToCommandList(Line);
    }

    private void Start()
    {
        DeactivateLine();
    }

    private void OnEnable()
    {
        Tick += DebugTick;
    }

    private void Update()
    {
        if(isInRhythm)
            CheckTick();
    }

    private static List<float> LineStringToCommandList(string s)
    {
        string[] commandListButStrings = s.Split(' ');
        List<float> toReturn = commandListButStrings.Select(ToFloat).ToList();
        return toReturn;
    }

    private void CheckTick()
    {
        if (source.time >= nextTick)
        {
            currentCommandIndex++;
            if (currentCommandIndex >= commands.Count)
            {
                StopRhythm();
                End?.Invoke(LineName);
            }
            else
                SetNextTickTime();
        }
    }

    private void DebugTick(string ln)
    {
//        Debug.Log($"Tick! (at {source.time})");
    }

    private void SetNextTickTime()
    {
        lastTick = nextTick;
        nextTick += MusicUtility.BeatsToSeconds(Mathf.Abs(commands[currentCommandIndex]), bpm);
        if(commands[currentCommandIndex] > 0)
            Tick?.Invoke(LineName);
    }
    
    public void StartRhythm()
    {
        currentCommandIndex = 0;
        isInRhythm = true;
        lastTick = 0;
        SetNextTickTime();
        source.time = 0;
        source.Play();
    }

    public void PauseRhythm()
    {
        isInRhythm = false;
        source.Pause();
    }

    public void ResumeRhythm()
    {
        isInRhythm = true;
        source.Play();
    }

    public void StopRhythm()
    {
        isInRhythm = false;
        source.Stop();
        currentCommandIndex = 0;
    }
    
    public void DeactivateLine()
    {
        source.volume = 0;
    }

    public void ActivateLine()
    {
        source.volume = initialVolume;
    }

    private static float ToFloat(string value)
    {
        if (value == "" || value == "\n" || value == " ") return 0;
        if (value.Contains('/'))
        {
            String[] numDen = value.Split('/');
            if (numDen[1] == "0") return 0; 
            return float.Parse(numDen[0]) / float.Parse(numDen[1]);
        }

        if (float.TryParse(value, out var toReturn))
        {
            return toReturn;
        }

        return 0;
    }

    public static void CancelEvents()
    {
        Tick = null;
        End = null;
    }
}
