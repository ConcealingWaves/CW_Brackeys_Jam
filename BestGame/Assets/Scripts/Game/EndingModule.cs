using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingModule : MonoBehaviour
{
    [SerializeField] private Beatmap mapToWatch;
    [SerializeField] private Transform endScreen;

    private void OnEnable()
    {
        Beatmap.OnEnd += CheckEnd;
    }

    private void OnDisable()
    {
        Beatmap.OnEnd -= CheckEnd;
    }

    private void CheckEnd(Beatmap bm)
    {
        if (bm == mapToWatch)
        {
            endScreen.gameObject.SetActive(true);
        }
    }
    
}
