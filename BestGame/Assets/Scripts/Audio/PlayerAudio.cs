using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthHaver))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip playerDamageClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip absorbClip;

    private AudioSource source;
    private HealthHaver player;
    private Absorbable absorbable;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        player = GetComponent<HealthHaver>();
        absorbable = GetComponent<Absorbable>();
    }

    private void OnEnable()
    {
        player.OnThisHit += PlayDamageClip;
        player.OnThisDie += PlayDeathClip;
        if(absorbable!=null)
            absorbable.OnThisAbsorb += PlayAbsorbClip;
    }

    private void OnDisable()
    {
        player.OnThisHit -= PlayDamageClip;
        player.OnThisDie -= PlayDeathClip;
        if(absorbable!=null)
            absorbable.OnThisAbsorb -= PlayAbsorbClip;
    }

    private void PlayDamageClip(float n, HealthHaver hh)
    {
        if(playerDamageClip!=null)
            source.PlayOneShot(playerDamageClip);
    }

    private void PlayDeathClip(HealthHaver hh)
    {
        if(playerDeathClip!=null)
            source.PlayOneShot(playerDeathClip);
    }
    
    private void PlayAbsorbClip()
    {
        if (absorbClip != null)
        {
            source.PlayOneShot(absorbClip, 0.4f);
        }
    }
    
}
