using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    public float tiempo;
    public int puntajeMin;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("AudioBackground"))
        {
            Destroy(GameObject.Find("AudioBackground"));
        }
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempo > 0)
        {
            tiempo -= Time.deltaTime;
        }
        else
        {
            if(puntajeMin >= GameManager.Puntos)
            {
                PlayerPrefs.SetString("EscenaAnterior", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Generador QR");
            }
        }
    }
}
