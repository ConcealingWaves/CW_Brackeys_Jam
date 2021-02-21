using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFader : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> faders;

    [SerializeField] private float fadeTimeEach;

    [SerializeField] private float fadeWaitInterval;

    private void Start()
    {
        StartCoroutine(FadeSequences());
    }

    IEnumerator FadeSequences()
    {
        foreach (var textToFade in faders)
        {
            yield return new WaitForSeconds(fadeWaitInterval);
            textToFade.gameObject.SetActive(true);
            float startTime = Time.time;
            Color originalColor = textToFade.color;
            while (Time.time - startTime <= fadeTimeEach)
            {
                textToFade.color = Color.Lerp(new Color(1,1,1,0),originalColor,Mathf.Sin((Time.time-startTime)/fadeTimeEach*Mathf.PI));
                yield return null;
            }
            textToFade.gameObject.SetActive(false);
        }
    }
}
