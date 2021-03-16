using com.baiba.core.lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.GameManager
{
    public class GameManager : MonoBehaviour
    {
        //Instanciamiento Estatico
        private static GameManager _instance;
        public static GameManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if(_instance == null)
                    {
                        GameObject GameManager = new GameObject();
                        _instance = GameManager.AddComponent<GameManager>();
                        DontDestroyOnLoad(GameManager);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //Fin Instanciamiento Estatico

        private void Star()
        {
            Language lang = Language.instance;
            lang.Init("es");
        }

        //Declaracion de Variables Get Set
        private static int ordenesCountLevel;
        public static int OrdenesCountLevel
        {
            get { return ordenesCountLevel; }
            set { ordenesCountLevel = value; }
        }
    }
}

