using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringModule : MonoBehaviour
{
    public delegate void ScoreChangeAction(float to);

    public event ScoreChangeAction OnChangeTo;

    [SerializeField] private FloatVariable score;
    [SerializeField] private FloatVariable scoreMultiplier;

    [SerializeField] private Absorber absorbMultiplier;

    public float highestMultiplier;

    public float Score
    {
        get => score.Value;
        private set => score.Value = value;
    }

    private void Awake()
    {

    }

    private void OnEnable()
    {
        HealthHaver.OnDie += CalculateScoreFromDeath;
    }

    private void OnDisable()
    {
        HealthHaver.OnDie -= CalculateScoreFromDeath;
    }

    private void Update()
    {
        UpdateCombo();
    }

    private void CalculateScoreFromDeath(HealthHaver dead)
    {
        Enemy deadEnemy = dead.GetComponent<Enemy>();
        if (deadEnemy != null && deadEnemy.enabled)
        {
            AddScore(deadEnemy.Value * scoreMultiplier.Value);
        }
    }

    private void UpdateCombo()
    {
        scoreMultiplier.Value = absorbMultiplier == null ? 0 : absorbMultiplier.NumberAbsorbed;
        highestMultiplier = Mathf.Max(highestMultiplier, scoreMultiplier.Value);
    }

    private void AddScore(float s)
    {
        score.Value += s;
        OnChangeTo?.Invoke(score.Value);
    }
    
}
