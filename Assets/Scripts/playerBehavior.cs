using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerBehavior : MonoBehaviour
{
    GameObject[] cantidadGO;
    Vector3 movement;
    int currentScene;
    int cant;
    float speed;
    AsyncOperation cargaEsc1;
    AsyncOperation cargaEsc2;
    void Start()
    {
        speed = 10.0f;
        currentScene = 0;
        SetCant();
        StartCoroutine("CargaEscena");
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        transform.Translate(movement * speed * Time.deltaTime);
       /* cantidadGO = GameObject.FindGameObjectsWithTag("puntos");
        Debug.Log(cantidadGO.Length);
        cant = cantidadGO.Length;*/
        Debug.Log(cargaEsc1.progress);
    }
    public void SetCant()
    {
        cantidadGO = GameObject.FindGameObjectsWithTag("puntos");
        Debug.Log(cantidadGO.Length);
        cant = cantidadGO.Length;
    }
    public void Restar()
    {
        cant--;
        if (cant == 0)
        {
            switch (currentScene)
            {
                case (0):
                    
                    if (cargaEsc1.progress >= 0.9f)
                    {
                        cargaEsc1.allowSceneActivation = true;
                        currentScene++;
                        SetCant();
                        break;
                    }
                    break;

                    
                case (1):
                    if (cargaEsc2.progress >= 0.9f) {
                        cargaEsc2.allowSceneActivation = true;
                        currentScene++;
                        SetCant();
                        break;
                    }
                    break;
                    
                    
            }
            
        }
    }
    IEnumerator CargaEscena()
    {
        cargaEsc1 = SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Additive);
        cargaEsc2 = SceneManager.LoadSceneAsync("Scene3", LoadSceneMode.Additive);
        cargaEsc1.allowSceneActivation = false;
        cargaEsc2.allowSceneActivation = false;
        while(!cargaEsc1.isDone && !cargaEsc2.isDone)
        {
            yield return null;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetCant();
    }
}
