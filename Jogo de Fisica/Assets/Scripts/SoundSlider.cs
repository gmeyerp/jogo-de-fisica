using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] bool isBGM;
    [SerializeField] Slider slider;
    public void ChangeVolume(float volume)
    {
        if (isBGM)
        {
            SoundManager.instance.SetBGMVolume(volume);
        }
        else
        {
            SoundManager.instance.SetSFXVolume(volume);
        }
    }

    private void OnEnable()
    {
        if (isBGM)
        {
            slider.value = SoundManager.instance.GetBGMVolume();
        }
        else
        {
            slider.value = SoundManager.instance.GetSFXVolume();
        }
    }
}
