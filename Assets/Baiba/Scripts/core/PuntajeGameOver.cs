using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntajeGameOver : MonoBehaviour
{
    public Text puntos;
    // Start is called before the first frame update
    void Start()
    {
        puntos.text = "TU PUNTAJE FUE\n" + GameManager.Puntos.ToString();
    }
}
