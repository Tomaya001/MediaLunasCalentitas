using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizarPJScript : MonoBehaviour
{
    public GameObject[] piel;
    public Color[] colors;

    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarColorPiel(string dir)
    {
        
        switch (dir)
        {
            case "sig":
                CambiarColorVector(piel);
                i++;
                break;
            case "ant":
                CambiarColorVector(piel);
                i--;
                break;
        }
    }

    void CambiarColorVector(GameObject[] vector)
    {
        if(i >= colors.Length)
        {
            i = 0;
        }
        if(i < 0)
        {
            i = colors.Length - 1;
        }
        foreach (GameObject g in vector)
        {
            g.gameObject.GetComponent<Renderer>().material.color = colors[i];
        }
    }

    public void AplicarSkin()
    {
        PlayerPrefs.SetString("colorSkin", piel[0].gameObject.GetComponent<Renderer>().material.color.ToString());
        PlayerPrefs.Save();
    }
}
