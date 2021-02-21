using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    NORMAL, HARDCORE
}
public class GlobalStats : MonoBehaviour
{
    public static GlobalStats instance;

    private float highScore;
    private Difficulty selectedDifficulty;

    public float HighScore
    {
        get => highScore;
        set => highScore = value;
    }

    public Difficulty SelectedDifficulty
    {
        get => selectedDifficulty;
        set => selectedDifficulty = value;
    }

    public static float DifficultyMultiplier(Difficulty d)
    {
        return d switch
        {
            Difficulty.NORMAL => 1,
            Difficulty.HARDCORE => 5,
            _ => 1
        };
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
