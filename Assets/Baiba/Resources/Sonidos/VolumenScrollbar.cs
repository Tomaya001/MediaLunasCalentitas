using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumenScrollbar : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(mixer.name))
        {
            PlayerPrefs.SetFloat(mixer.name, 1f);
            Load();
        }
        else
        {
            Load();
        }

    }

    private void Load()
    {
        slider.value = PlayerPrefs.GetFloat(mixer.name);
    }

    public void ChangeVolumen(float sliderValue)
    {
        float aux = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("Volumen", aux);
        PlayerPrefs.SetFloat(mixer.name, aux);
    }
}
