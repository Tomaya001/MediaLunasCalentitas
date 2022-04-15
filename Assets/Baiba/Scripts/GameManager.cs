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
            Time.timeScale = 1f;
            ordenesCorrectas = 0;
            ordenesPerdidas = 0;
            dificultad = 1f;
            CargarNivel();
        }
        //Fin Instanciamiento Estatico

        private void Star()
        {

        }


        private void Update()
        {
            if(ordenesPerdidas >= MaxOrdenesPerdidas)
            {
                StopAllCoroutines();
                Time.timeScale = 0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(CONST.SCENES.LOSE);
                ordenesPerdidas = 0;
            }
        
        }

        //Declaracion de Variables Get Set
        private static float dificultad;
        public static float Dificultad
        {
            get { return dificultad; }
            set { dificultad = value; }
        }

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
            get { return ordenesPerdidas; }
            set { ordenesPerdidas = value; }
        }


        /* ---- UI CONTROLLER ----- */
        public static List<Text> UIOrdenes = new List<Text>();
        public static List<string> clientesActivos = new List<string>();
        public static void CargarUIPrincipal()
        {
            GameObject aux = GameObject.FindGameObjectWithTag(CONST.TAG.CANVASORDENES);
            for (int i = 0; i < aux.transform.childCount; i++)
            {
                if(aux.transform.GetChild(i).GetComponent<Text>())
                {
                    UIOrdenes.Add(aux.transform.GetChild(i).GetComponent<Text>());
                    aux.transform.GetChild(i).gameObject.name = "UILibre";
                    aux.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public static void MostrarOrden(GameObject cliente)
        {
            for (int i = 0; i < UIOrdenes.Count; i++)
            {
                if (!UIOrdenes[i].gameObject.activeSelf)
                {
                    if(UIOrdenes[i].gameObject.name == "UILibre")
                    {
                        if(UIOrdenes[i].gameObject.name != cliente.name)
                        {
                            if (!clientesActivos.Contains(cliente.name))
                            {
                                foreach (GameObject g in listaOrdenes.Keys)
                                {
                                    if (g == cliente)
                                    {
                                        UIOrdenes[i].text = null;
                                        for (int f = 0; f < listaOrdenes[g].ingredientes.Length; f++)
                                        {
                                            UIOrdenes[i].text += listaOrdenes[g].ingredientes[f].nombre + '\n';
                                        }
                                        UIOrdenes[i].gameObject.name = cliente.name;
                                        clientesActivos.Add(cliente.name);
                                        UIOrdenes[i].gameObject.SetActive(true);
                                        return;
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }

        }

        public static void OcultarOrden(GameObject cliente)
        {
            foreach (Text t in UIOrdenes)
            {
                if ((t.gameObject.activeSelf) & (t.gameObject.name.Contains("Cliente")))
                {
                    if(t.gameObject.name == cliente.name)
                    {
                        t.gameObject.name = "UILibre";
                        clientesActivos.Remove(cliente.name);
                        t.text = null;
                        t.gameObject.SetActive(false);
                        return;
                    }
                }
            }
        }

        public static void DebugTexto(List<Text> Vector)
        {
            foreach (Text t in Vector)
            {
                Debug.Log(t.gameObject.name);
            }
        }
    }


}

