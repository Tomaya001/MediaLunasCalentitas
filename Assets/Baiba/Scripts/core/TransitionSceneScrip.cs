using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace com.baiba.core
{
    public class TransitionSceneScrip : MonoBehaviour
    {
        public bool isPasive;
        public float time = 0;
        public string scene;

        private AsyncOperation carga;
        private string sceneCarga;

        private void Start()
        {
            if(isPasive)
            {
                if (time != 0)
                {
                    IEnumerator aux = LoadSceneforTime(scene, time, LoadSceneMode.Single);
                    StartCoroutine(aux);
                }
                else
                {
                    IEnumerator aux = PreLoadSceneAsync(scene, LoadSceneMode.Additive);
                    StartCoroutine(aux);
                    LoadSceneAsync(scene);
                }
                    
            }
        }

        public IEnumerator LoadSceneforTime(int scene, float time, LoadSceneMode load)
        {
            ConverScene(scene);
            carga = SceneManager.LoadSceneAsync(scene, load);
            carga.allowSceneActivation = false;
            yield return new WaitForSeconds(time);
            carga.allowSceneActivation = true;
        }
        public IEnumerator LoadSceneforTime(string scene, float time, LoadSceneMode load)
        {
            ConverScene(scene);
            carga = SceneManager.LoadSceneAsync(scene, load);
            carga.allowSceneActivation = false;
            yield return new WaitForSeconds(time);
            carga.allowSceneActivation = true;
        }

        public IEnumerator PreLoadSceneAsync(int scene,LoadSceneMode load)
        {
            ConverScene(scene);
            carga = SceneManager.LoadSceneAsync(scene,load);
            carga.allowSceneActivation = false;
            while (!carga.isDone)
                yield return null;
        }
        public IEnumerator PreLoadSceneAsync(string scene, LoadSceneMode load)
        {
            ConverScene(scene);
            carga = SceneManager.LoadSceneAsync(scene, load);
            sceneCarga = scene;
            carga.allowSceneActivation = false;
            while (!carga.isDone)
                yield return null;
        }

        public void LoadSceneAsync(string scene)
        {
            if(scene == sceneCarga)
            {
                carga.allowSceneActivation = true;
            }
        }

        private void ConverScene(int scene)
        {
            switch(scene)
            {
                case 1:
                    sceneCarga = CONST.SCENES.LOGO;
                    break;
                case 2:
                    sceneCarga = CONST.SCENES.MAINMENU;
                    break;
                case 3:
                    sceneCarga = CONST.SCENES.GAMEPLAY;
                    break;
            }
        }
        private void ConverScene(string scene)
        {
            sceneCarga = scene;
        }

    }

}

