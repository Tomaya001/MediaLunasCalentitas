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
                    IEnumerator aux = LoadSceneSimple(scene, LoadSceneMode.Single);
                    StartCoroutine(aux);
                }
                    
            }
        }

        public IEnumerator LoadSceneforTime(int scene, float time, LoadSceneMode load)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadSceneAsync(scene,load);
        }
        public IEnumerator LoadSceneforTime(string scene, float time, LoadSceneMode load)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadSceneAsync(scene, load);
        }

        public IEnumerator LoadSceneSimple(int scene,LoadSceneMode load)
        {
            yield return null;
            SceneManager.LoadSceneAsync(scene, load);
        }
        public IEnumerator LoadSceneSimple(string scene, LoadSceneMode load)
        {
            yield return null;
            SceneManager.LoadSceneAsync(scene, load);
        }
    }

}

