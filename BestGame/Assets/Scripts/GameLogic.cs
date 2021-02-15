using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PausePanel;
    public WaveSpawner waveSpawner;
    private bool isStart = false;
    private bool isEnd = false;
    private bool isPause = false;
    // Start is called before the first frame update
    void Awake()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart&&!isPause&&!isEnd)
        {
            if (Input.GetKeyDown(KeyCode.P))
                GamePause();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
                GameResume();
        }
    }

    public void GameStart()
    {

        isStart = true;
        isEnd = isPause = false;
        waveSpawner.GameStart();

    }
    public void GameOver()
    {
        isEnd = true;
        waveSpawner.GameOver();
    }

    public void GamePause()
    {
        if(isStart&&!isEnd)
        {
            isPause = true;
            PauseButton.SetActive(false);
            PausePanel.SetActive(true);
            waveSpawner.GamePause();
        }
    }
    public void GameResume()
    {
        if (isStart && !isEnd)
        {
            isPause = false;
            PauseButton.SetActive(true);
            PausePanel.SetActive(false);
            waveSpawner.GameResume();
        }
    }
    public void GameRestart()
    {
    }
}

