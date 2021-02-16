using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreText : MonoBehaviour
{
    public int increaseSeverityInterval = 10;
    public int stepsToFullyChangeColor = 20;
    public Color severityColor = Color.red;
    public float displayChangeSpeedPerSeverityInterval = 1.0f;
    public float maxFontSizeIncrease = 32;

    int displayedScore = 0;
    int actualScore = 0;
    TextMeshProUGUI textMesh;
    float baseFontSize = 1;
    Color baseColor = Color.white;

    public void ResetToZero()
    {
        SetScore(0);
        displayedScore = 0;
        UpdateDisplay();
    }

    public void SetScore(int amount)
    {
        actualScore = amount;
    }

    public int GetScore()
    {
        return actualScore;
    }

    public void AddScore(int amount)
    {
        SetScore(GetScore() + amount);
    }

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        baseFontSize = textMesh.fontSize;
        baseColor = textMesh.color;
    }

    void Start()
    {
        ResetToZero();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedScore != actualScore)
        {
            StepDisplayedScoreTowardsActual();
            UpdateDisplay();
        }
    }

    private void StepDisplayedScoreTowardsActual()
    {
        int stepsToTakeTowardsGoal = (int) Mathf.Ceil(GetIntervalsBetweenActualScoreAndDisplayed() * displayChangeSpeedPerSeverityInterval);
        displayedScore += stepsToTakeTowardsGoal;
    }

    private int GetIntervalsBetweenActualScoreAndDisplayed()
    {
        int diff = Mathf.Abs(actualScore - displayedScore);
        int intervals = (int) Mathf.Ceil(diff / (float) increaseSeverityInterval);
        return intervals;
    }

    private void UpdateDisplay()
    {
        textMesh.fontSize = baseFontSize + Mathf.Min(GetIntervalsBetweenActualScoreAndDisplayed(), maxFontSizeIncrease);
        textMesh.color = Color.Lerp(baseColor, severityColor, (float) GetIntervalsBetweenActualScoreAndDisplayed() / stepsToFullyChangeColor);
        textMesh.text = displayedScore.ToString();
    }
}
