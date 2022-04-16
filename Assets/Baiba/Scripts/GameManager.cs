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
        public Text txtPuntos;
        private int MaxOrdenesPerdidas;
        public List<Sprite> Iconos;
        public static List<Sprite> iconos;


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
            iconos = new List<Sprite>();
            iconos.AddRange(Iconos);
            Language lang = Language.instance;
            lang.Init("es");
            nivelJuego = NivelJuego;
            MaxOrdenesPerdidas = maxOrdenesPerdidas;
            Time.timeScale = 1f;
            ordenesCorrectas = 0;
            ordenesPerdidas = 0;
            dificultad = 1f;
            puntos = 0;
            CargarNivel();
        }
        //Fin Instanciamiento Estatico

        private void Star()
        {

        }


        private void Update()
        {
            txtPuntos.text = "Puntos\n" + puntos;
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

        private static int puntos;
        public static int Puntos
        {
            get { return puntos; }
            set { puntos = value; }
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
        public static List<GameObject> UIOrdenes = new List<GameObject>();
        public static List<string> clientesActivos = new List<string>();
        public static void CargarUIPrincipal()
        {
            GameObject aux = GameObject.FindGameObjectWithTag(CONST.TAG.CANVASORDENES);
            for (int i = 0; i < aux.transform.childCount; i++)
            {
                if(aux.transform.GetChild(i).GetComponentInChildren<Image>())
                {
                    for (int j = 0; j < aux.transform.GetChild(i).childCount; j++)
                    {
                        aux.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                    }
                    UIOrdenes.Add(aux.transform.GetChild(i).gameObject);
                    aux.transform.GetChild(i).gameObject.name = "UILibre";
                    aux.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public static Sprite BuscarIcono(string nombre, List<Sprite> sprites)
        {
            foreach (Sprite s in sprites)
            {
                if(s.name == nombre)
                {
                    return s;
                }
            }
            return null;
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
                                        for (int j = 0; j < UIOrdenes[i].transform.childCount; j++)
                                        {
                                            UIOrdenes[i].transform.GetChild(j).GetComponent<Image>().sprite = null;                                            
                                            for (int k = 0; k < listaOrdenes[g].ingredientes.Length; k++)
                                            {
                                                UIOrdenes[i].transform.GetChild(k).gameObject.SetActive(true);
                                                UIOrdenes[i].transform.GetChild(k).GetComponent<Image>().sprite =
                                                    BuscarIcono(listaOrdenes[g].ingredientes[k].nombre, iconos);
                                            }
                                            break;
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
            foreach (GameObject s in UIOrdenes)
            {
                if ((s.gameObject.activeSelf) & (s.gameObject.name.Contains("Cliente")))
                {
                    if(s.gameObject.name == cliente.name)
                    {
                        for (int i = 0; i < s.transform.childCount; i++)
                        {
                            s.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        s.gameObject.name = "UILibre";
                        clientesActivos.Remove(cliente.name);
                        s.gameObject.SetActive(false);
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

