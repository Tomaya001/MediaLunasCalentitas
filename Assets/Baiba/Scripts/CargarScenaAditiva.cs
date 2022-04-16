using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarScenaAditiva : MonoBehaviour
{
    private Camera camera;

    public void CargarEscena()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private IEnumerator CargarEscenaCorrutina()
    {
        AsyncOperation carga =  SceneManager.LoadSceneAsync("GamePlay", LoadSceneMode.Additive);
        while (!carga.isDone)
        {
            yield return null;
        }
        camera = Camera.main;
        SceneManager.UnloadSceneAsync("Customize");
        camera.GetComponent<Animator>().SetBool("Custom", true);
    }
}
