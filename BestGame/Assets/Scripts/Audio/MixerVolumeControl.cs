using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerVolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;


    public void SetMusicVolume(float slider)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Max(slider,0.0001f)) * 20);
    }
    
    public void SetSFXVolume(float slider)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(Mathf.Max(slider,0.0001f)) * 20);
    }
}
