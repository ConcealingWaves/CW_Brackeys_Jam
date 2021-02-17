using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    private const float FLOATING_POINT_LEEWAY = 0.01f;
    
    public int increaseSeverityInterval = 10;
    public int stepsToFullyChangeColor = 20;
    public Color severityColor = Color.red;
    public float displayChangeSpeedPerSeverityInterval = 1.0f;
    public float maxFontSizeIncrease = 32;
    [Space(10)] [SerializeField] private FloatVariable scoreToRead; 

    float displayedScore = 0;
    float actualScore = 0;
    TextMeshProUGUI textMesh;
    float baseFontSize = 1;
    Color baseColor = Color.white;

    public void ResetToZero()
    {
        SetScore(0);
        displayedScore = 0;
        UpdateDisplay();
    }

    public void SetScore(float amount)
    {
        actualScore = amount;
    }

    public float GetScore()
    {
        return actualScore;
    }

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        baseFontSize = textMesh.fontSize;
        baseColor = textMesh.color;
    }

    private void OnEnable()
    {
        if(scoreToRead!=null)
            scoreToRead.OnValueChangedTo += SetScore;
    }

    private void OnDisable()
    {
        if(scoreToRead!=null)
            scoreToRead.OnValueChangedTo -= SetScore;
    }

    void Start()
    {
        ResetToZero();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(displayedScore - actualScore)>=FLOATING_POINT_LEEWAY)
        {
            StepDisplayedScoreTowardsActual();
            UpdateDisplay();
        }
    }

    private void StepDisplayedScoreTowardsActual()
    {
        int stepsToTakeTowardsGoal = (int) Mathf.Ceil(GetIntervalsBetweenActualScoreAndDisplayed() * displayChangeSpeedPerSeverityInterval);
        displayedScore = displayedScore < actualScore ?  displayedScore+stepsToTakeTowardsGoal : displayedScore-stepsToTakeTowardsGoal;
    }

    private int GetIntervalsBetweenActualScoreAndDisplayed()
    {
        float diff = Mathf.Abs(actualScore - displayedScore);
        int intervals = (int) Mathf.Ceil(diff / (float) increaseSeverityInterval);
        return intervals;
    }

    private void UpdateDisplay()
    {
        textMesh.fontSize = baseFontSize + Mathf.Min(GetIntervalsBetweenActualScoreAndDisplayed(), maxFontSizeIncrease);
        textMesh.color = Color.Lerp(baseColor, severityColor, (float) GetIntervalsBetweenActualScoreAndDisplayed() / stepsToFullyChangeColor);
        textMesh.text = Mathf.Round(displayedScore).ToString(CultureInfo.InvariantCulture);
    }
}
