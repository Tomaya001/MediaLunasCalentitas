using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public GameObject[] piel;
    // Start is called before the first frame update
    void Start()
    {
        string[] aux = PlayerPrefs.GetString("colorSkin").Split(';', ',','(',')');
        foreach (GameObject g in piel)
        {
            g.gameObject.GetComponent<Renderer>().material.color = new Color(float.Parse(aux[1]), float.Parse(aux[2]), float.Parse(aux[3]));
        }
    }

    
}
