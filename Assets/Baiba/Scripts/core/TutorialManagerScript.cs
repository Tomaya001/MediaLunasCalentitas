using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> pantallas;
    int i;

    private void Start()
    {
        i = 0;
        pantallas[i].SetActive(true);
    }

    private void Update()
    {
        if((Input.touchCount == 1) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            pantallas[i].SetActive(false);
            i++;
            if (i < pantallas.Count)
            {
                pantallas[i].SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                SceneManager.LoadScene("GamePlay");
            }
        }
    }


}
