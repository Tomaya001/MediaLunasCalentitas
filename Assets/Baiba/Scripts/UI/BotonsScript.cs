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
        SceneManager.LoadScene(PlayerPrefs.GetString("EscenaAnterior"), LoadSceneMode.Single);
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
    }

    public void CerrarMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
