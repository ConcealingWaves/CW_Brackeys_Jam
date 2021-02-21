using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScoresDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private TextMeshProUGUI instruments;
    [SerializeField] private Color completionColor;
    [SerializeField] private Color defaultColor;

    private void Start()
    {
        score.text = Mathf.Round(GlobalStats.instance.HighScore).ToString();
        score.color = defaultColor;
        instruments.text = GlobalStats.instance.MAXInstrumentsEver.ToString();
        instruments.color = GlobalStats.instance.EverCompleted ? completionColor : defaultColor;
    }
}
