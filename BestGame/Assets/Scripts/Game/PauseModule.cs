using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseModule : MonoBehaviour
{
    [SerializeField] private KeyCode pauseButton;
    [SerializeField] private Beatmap beatmapToPause;
    [SerializeField] private Transform pausePanel;

    private bool isPaused;

    private void Awake()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if(isPaused) Unpause();
        else Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        beatmapToPause.Pause();
        pausePanel.gameObject.SetActive(true);
        isPaused = true;
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        beatmapToPause.Unpause();
        pausePanel.gameObject.SetActive(false);
        isPaused = false;
    }
}
