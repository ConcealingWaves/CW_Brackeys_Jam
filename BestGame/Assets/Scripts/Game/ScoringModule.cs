using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringModule : MonoBehaviour
{
    public delegate void ScoreChangeAction(float to);

    public event ScoreChangeAction OnChangeTo;
    
    private float score;
    private float scoreMultiplier;
    [SerializeField] private Absorber absorbMultiplier;

    public float Score
    {
        get => score;
        private set => score = value;
    }

    private void Awake()
    {
        score = 0;
        scoreMultiplier = 1;
    }

    private void OnEnable()
    {
        HealthHaver.OnDie += CalculateScoreFromDeath;
        AbsorbBase.OnChangeAbsorbNumber += UpdateCombo;
    }

    private void OnDisable()
    {
        HealthHaver.OnDie -= CalculateScoreFromDeath;
        AbsorbBase.OnChangeAbsorbNumber -= UpdateCombo;
    }

    private void CalculateScoreFromDeath(HealthHaver dead)
    {
        Enemy deadEnemy = dead.GetComponent<Enemy>();
        if (deadEnemy != null)
        {
            AddScore(deadEnemy.Value * scoreMultiplier);
        }
    }

    private void UpdateCombo()
    {
        scoreMultiplier = absorbMultiplier.NumberAbsorbed + 1;
    }

    private void AddScore(float s)
    {
        score += s;
        OnChangeTo?.Invoke(score);
    }
    
}
