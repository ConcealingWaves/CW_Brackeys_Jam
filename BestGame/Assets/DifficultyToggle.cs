using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyToggle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpro;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hardcoreColor;

    private void Start()
    {
        ToggleDifficulty();
        ToggleDifficulty();
    }

    public void ToggleDifficulty()
    {
        if(GlobalStats.instance.SelectedDifficulty == Difficulty.NORMAL)
            ToHardcore();
        else
            ToNormal();

    }

    private void ToNormal()
    {
        GlobalStats.instance.SelectedDifficulty = Difficulty.NORMAL;
        tmpro.text = "Normal";
        tmpro.color = normalColor;
    }

    private void ToHardcore()
    {
        GlobalStats.instance.SelectedDifficulty = Difficulty.HARDCORE;
        tmpro.text = "Hardcore";
        tmpro.color = hardcoreColor;
    }
}
