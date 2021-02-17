using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTraverser : MonoBehaviour
{
    public static SceneTraverser instance;

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

    public void GoToScene(int to)
    {
        BeatmapLine.CancelEvents(); //whatever
        SceneManager.LoadSceneAsync(to);
    }
}
