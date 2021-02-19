using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreField;
    [SerializeField] private TextMeshPro comboField;
    [Space(10)] [SerializeField] private float timeToFade;
    [SerializeField] private float heightToFade;

    private Vector2 originalPos;

    private void Start()
    {
        originalPos = transform.position;
        StartCoroutine(DisappearSequence());
    }

    public void DisplayScore(float f)
    {
        scoreField.text = Mathf.RoundToInt(f).ToString();
    }
    
    public void DisplayCombo(int c)
    {
        comboField.text = "x"+c;
    }

    IEnumerator DisappearSequence()
    {
        float start = Time.time;
        Vector2 toPos = originalPos + new Vector2(0,1)*heightToFade;
        Color originalScoreColor = scoreField.color;
        Color originalComboColor = comboField.color;
        while (Time.time - start < timeToFade)
        {
            float t = (Time.time - start) / timeToFade;
            transform.position = Vector2.Lerp(originalPos,toPos, t);
            scoreField.color = new Color(originalScoreColor.r,originalScoreColor.g,originalScoreColor.b,Mathf.Lerp(1,0,t));
            comboField.color = new Color(originalComboColor.r,originalComboColor.g,originalComboColor.b,Mathf.Lerp(1,0,t));
            yield return null;
        }
        gameObject.SetActive(false);
    }
    
}
