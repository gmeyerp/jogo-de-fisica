using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.Stop();
        bgm.clip = clip;
        bgm.Play();
        bgm.loop = true;
    }

    public void SetBGMVolume(float volume)
    {
        bgm.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfx.volume = volume;
    }

    public float GetBGMVolume()
    {
        return bgm.volume;
    }

    public float GetSFXVolume()
    {
        return sfx.volume;
    }
}
