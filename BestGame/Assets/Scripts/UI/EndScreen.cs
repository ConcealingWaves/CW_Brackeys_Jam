using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whyEndField;
    [SerializeField] private ScoreText scoreField;
    [SerializeField] private ScoreText comboField;
    [SerializeField] private float updateGap;
    [SerializeField] private AudioSource endScreenAudio;
    [SerializeField] private AudioClip endScreenTallyClip;

    public void SetEndText(string endText)
    {
        whyEndField.text = endText;
    }

    public void SetScore(float score)
    {
        scoreField.SetScore(score);
    }

    public void SetCombo(float combo)
    {
        comboField.SetScore(combo);
    }

    public void StartSequence(string s, float f, float c)
    {
        StartCoroutine(EndSequence(s,f,c));
    }

    IEnumerator EndSequence(string endText, float score, float combo)
    {
        SetEndText(endText);
        yield return new WaitForSeconds(updateGap);
        SetScore(score);
        endScreenAudio.PlayOneShot(endScreenTallyClip);
        yield return new WaitForSeconds(updateGap);
        SetCombo(combo);
    }
}
