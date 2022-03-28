using com.baiba.core.lang;
using com.baiba.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baiba.GameManager
{
    public class GameManager : MonoBehaviour
    {
        //Declaracion de Variables Publicas
        public string NivelJuego;
        private static string nivelJuego;
        public int maxOrdenesPerdidas;
        private int MaxOrdenesPerdidas;

        //Instanciamiento Estatico
        private static GameManager _instance;
        public static GameManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
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
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Language lang = Language.instance;
            lang.Init("es");
            nivelJuego = NivelJuego;
            MaxOrdenesPerdidas = maxOrdenesPerdidas;
            Time.timeScale = 1;
            CargarNivel();
        }
        //Fin Instanciamiento Estatico

        private void Star()
        {

        }

        public List<Text> aux;
        private void Update()
        {
            aux = UIOrdenes;

            if(ordenesPerdidas >= MaxOrdenesPerdidas)
            {
                Time.timeScale = 0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(CONST.SCENES.LOSE);
                ordenesPerdidas = 0;
            }
        
        }

        //Declaracion de Variables Get Set
        private static string json;
        public static string Json
        {
            get { return json; }
            set { json = value; }
        }
        public static void CargarJson(string archivo)
        {
            Json = (AssetLoader.GetAsset<TextAsset>(CONST.RESOURCES.FILES_FOLDER + archivo)).ToString();
        }

        private static Level nivel;
        public static Level Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public static void CargarNivel()
        {
            CargarJson(nivelJuego);
            Nivel = JsonUtility.FromJson<Level>(Json);
        }

        /*private static List<Orden> listaOrdenes = new List<Orden>();
        public static List<Orden> ListaOrdenes
        {
            get { return listaOrdenes; }
            set { ListaOrdenes = listaOrdenes; }
        }*/
        private static Dictionary<GameObject,Orden> listaOrdenes = new Dictionary<GameObject, Orden>();
        public static Dictionary<GameObject, Orden> ListaOrdenes
        {
            get { return listaOrdenes; }
            set { ListaOrdenes = listaOrdenes; }
        }
        private static int ordenesCountLevel;
        public static int OrdenesCountLevel
        {
            get { return ordenesCountLevel; }
            set { ordenesCountLevel = value; }
        }

        private static int ordenesCorrectas;
        public static int OrdenesCorrectas
        {
            get { return ordenesCorrectas; }
            set { ordenesCorrectas = value; }
        }

        private static int ordenesPerdidas;
        public static int OrdenesPerdidas
        {
            get { return OrdenesPerdidas; }
            set { ordenesPerdidas = value; }
        }



        public static List<Text> UIOrdenes = new List<Text>();
      /*-----------------------------------*/  
        public static void CargarUIPrincipal()
        {
            GameObject aux = GameObject.FindGameObjectWithTag(CONST.TAG.CANVASORDENES);
            for (int i = 0; i < aux.transform.childCount; i++)
            {
                if(aux.transform.GetChild(i).GetComponent<Text>())
                {
                    UIOrdenes.Add(aux.transform.GetChild(i).GetComponent<Text>());
                    aux.transform.GetChild(i).GetComponent<Text>().text = "Pase";
                    aux.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        public static void MostrarOrden(GameObject cliente)
        {
            foreach (Text t in UIOrdenes)
            {
                Debug.Log(UIOrdenes);
                if ((!t.gameObject.activeSelf) & (t.text != "pase"))
                {
                    t.text = null;
                    foreach (GameObject g in listaOrdenes.Keys)
                    {
                        if (g == cliente)
                        {
                            for (int i = 0; i < listaOrdenes[g].ingredientes.Length; i++)
                            {
                                t.text += listaOrdenes[g].ingredientes[i].nombre + '\n';
                            }
                        }
                    }
                    t.gameObject.SetActive(true);
                    break;
                }
            }

        }
    }


}

