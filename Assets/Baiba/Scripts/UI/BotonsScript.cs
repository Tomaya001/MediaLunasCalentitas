using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonsScript : MonoBehaviour
{
    public void CargarEscena(string scena)
    {
        PlayerPrefs.SetString("EscenaAnterior", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scena, LoadSceneMode.Single);
    }

    public void CargarEscenaAnterior()
    {
        if (PlayerPrefs.GetString("EscenaAnterior") != null)
            SceneManager.LoadScene(PlayerPrefs.GetString("EscenaAnterior"), LoadSceneMode.Single);
        else
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void AbrirURL(string url)
    {
        Application.OpenURL(url);
    }

    public void CargarMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void AbrirMenu(GameObject menu)
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CerrarMenu(GameObject menu)
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BotonJugar()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            if(PlayerPrefs.GetInt("Tutorial") == 0)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                SceneManager.LoadScene("GamePlay");
            }
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
