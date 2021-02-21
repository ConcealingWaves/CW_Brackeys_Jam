using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(HealthHaver))]
public class EnemyAnimations : MonoBehaviour
{
    private Beatmap mapToRead;
    private HealthHaver healthHaver;
    [Header("Pulsation")]
    [SerializeField] private float pulsateTo;
    [SerializeField] private float pulsateCycleBeats;
    private float pulsateCycleSeconds;
    private Vector3 originalScale;
    private Vector3 pulsateToScale;
    [Space(10)][Header("Hit Glow")]
    [SerializeField] private List<SpriteRenderer> sprites;
    [SerializeField] private Color hitColor;
    [SerializeField] private float hitGlowTime;
    private Color originalColor;

    [Space(10)] [Header("Death Explosion")] [SerializeField]
    private Transform deathExplosion;

    private IEnumerator hitSequence;
    
    private void Awake()
    {
        healthHaver = GetComponent<HealthHaver>();
        originalScale = transform.localScale;
        pulsateToScale = originalScale * pulsateTo;
        sprites.AddRange(GetComponents<SpriteRenderer>());
        originalColor = sprites[0].color;
    }

    private void OnEnable()
    {
        healthHaver.OnThisHit += HitGlow;
        healthHaver.OnThisDie += SpawnExplosion;
    }
    private void OnDisable()
    {
        healthHaver.OnThisHit -= HitGlow;
        healthHaver.OnThisDie -= SpawnExplosion;
    }

    private void Start()
    {
        mapToRead = FindObjectOfType<Beatmap>();
        pulsateCycleSeconds = MusicUtility.BeatsToSeconds(pulsateCycleBeats, mapToRead.Bpm);
    }

    private void Update()
    {
        Pulsate();
    }

    public void UpdateColor(Color c)
    {
        originalColor = c;
    }
    private void Pulsate()
    {
        float timeInCycle = (mapToRead.TimeSinceStart % pulsateCycleSeconds)/pulsateCycleSeconds;
        float sinterp = Mathf.Cos(timeInCycle * 2 * Mathf.PI) / 2 + 0.5f;
        Vector3 toScale = Vector3.Lerp(originalScale,pulsateToScale,sinterp);
        transform.localScale = toScale;
    }

    private void HitGlow(float n, HealthHaver na)
    {
        if(hitSequence!=null) StopCoroutine(hitSequence);
        hitSequence = HitGlowSequence(n, na);
        StartCoroutine(hitSequence);
    }

    IEnumerator HitGlowSequence(float n, HealthHaver na)
    {
        float startTime = Time.time;
        while (Time.time - startTime <= hitGlowTime)
        {
            foreach (var v in sprites)
            {
                v.color = Color.Lerp(hitColor, originalColor, Mathf.Cos((Time.time-startTime)/hitGlowTime * 2 * Mathf.PI)/2 + 0.5f);
            }

            yield return null;
        }
        foreach (var v in sprites)
        {
            v.color = originalColor;
        }
    }

    private void SpawnExplosion(HealthHaver hh)
    {
        Transform t = Instantiate(deathExplosion, hh.transform.position, Quaternion.identity);
        StartCoroutine(KillAfter(3, t));
    }

    IEnumerator KillAfter(float s, Transform t)
    {
        yield return new WaitForSeconds(s);
        Destroy(t.gameObject);
    }
}
