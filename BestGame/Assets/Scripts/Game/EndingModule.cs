using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingModule : MonoBehaviour
{
    [SerializeField] private Beatmap mapToWatch;
    [SerializeField] private HealthHaver playerToWatch;
    [SerializeField] private ScoringModule scoreToRead;
    [SerializeField] private EndScreen endScreen;
    [TextArea(2,8)]
    [Space] [SerializeField] private List<String> songEndStrings;
    [TextArea(2,8)]
    [Space] [SerializeField] private List<String> deathEndStrings;

    private void OnEnable()
    {
        Beatmap.OnEnd += CheckEnd;
        HealthHaver.OnDie += CheckDieEnd;
    }

    private void OnDisable()
    {
        Beatmap.OnEnd -= CheckEnd;
        HealthHaver.OnDie -= CheckDieEnd;
    }

    private void CheckEnd(Beatmap bm)
    {
        if (bm == mapToWatch)
        {
            endScreen.gameObject.SetActive(true);
            endScreen.StartSequence(songEndStrings[Random.Range(0,songEndStrings.Count)], scoreToRead.Score, scoreToRead.highestMultiplier);
        }
    }

    private void CheckDieEnd(HealthHaver hh)
    {
        if (hh == playerToWatch)
        {
            endScreen.gameObject.SetActive(true);
            endScreen.StartSequence(deathEndStrings[Random.Range(0,deathEndStrings.Count)], scoreToRead.Score, scoreToRead.highestMultiplier);
        }
    }
    
}
