using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumenScrollbar : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string tagMixer;
    [SerializeField] Slider slider;

    private void Start()
    {
        if(!PlayerPrefs.HasKey(tagMixer))
        {
            PlayerPrefs.SetFloat(tagMixer, Mathf.Log10(slider.value) * 20);
        }
        float aux = PlayerPrefs.GetFloat(tagMixer);
        slider.value = aux;
        mixer.SetFloat("MyExposedParam", Mathf.Log10(aux) * 20);

    }

    public void SetVolumen(float sliderValue)
    {
        mixer.SetFloat("MyExposedParam", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(tagMixer, sliderValue);
        Debug.Log(PlayerPrefs.GetFloat(tagMixer));
    }
}
