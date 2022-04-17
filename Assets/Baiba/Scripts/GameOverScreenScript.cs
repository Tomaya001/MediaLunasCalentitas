using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    [SerializeField] float tiempo;
    [SerializeField] int puntajeMin;
    [SerializeField] Slider slider;
    [SerializeField] GameObject btn1;
    [SerializeField] GameObject btn2;
    [SerializeField] GameObject panelCarga;

    AsyncOperation carga;
    bool temporizador;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("AudioBackground"))
        {
            Destroy(GameObject.Find("AudioBackground"));
        }

        if(GameManager.Puntos >= puntajeMin)
        {
            btn1.SetActive(false);
            btn2.SetActive(false);
            panelCarga.SetActive(true);
            Time.timeScale = 1f;
            carga = SceneManager.LoadSceneAsync("Generador QR");
            carga.allowSceneActivation = false;
            temporizador = true;
            StartCoroutine(CargarEscena());
        }
        else
        {
            btn1.SetActive(true);
            btn2.SetActive(true);
            panelCarga.SetActive(false);
            Time.timeScale = 0f;
            temporizador = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (temporizador)
        {
            if (tiempo > 0)
            {
                tiempo -= Time.deltaTime;
            }
            else
            {
                PlayerPrefs.SetString("EscenaAnterior", SceneManager.GetActiveScene().name);
                carga.allowSceneActivation = true;
            }
        }
        
    }

    IEnumerator CargarEscena()
    {
        while (!carga.isDone)
        {
            slider.value = Mathf.Clamp01(Mathf.InverseLerp(10f, 0f, tiempo) / 0.9f);
            yield return null;
        }
    }
}
