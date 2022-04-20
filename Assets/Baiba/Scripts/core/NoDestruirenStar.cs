using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NoDestruirenStar : MonoBehaviour
{
    public AudioMixer mixerMaster;
    public AudioMixer mixerSFX;

    private static NoDestruirenStar instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

# if UNITY_EDITOR
        PlayerPrefs.SetInt("Tutorial", 1);
#endif

        float sfx;
        float master;

        if (PlayerPrefs.HasKey("VolumenSFX"))
            sfx = PlayerPrefs.GetFloat("VolumenSFX");
        else
            sfx = 0.8f;

        if (PlayerPrefs.HasKey("VolumenMaster"))
            master = PlayerPrefs.GetFloat("VolumenMaster");
        else
            master = 0.8f;


        mixerMaster.SetFloat("MyExposedParam", Mathf.Log10(master) * 20);
        mixerSFX.SetFloat("MyExposedParam", Mathf.Log10(sfx) * 20);

    }


}
