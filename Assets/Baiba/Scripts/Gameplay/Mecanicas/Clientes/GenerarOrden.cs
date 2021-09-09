using com.baiba.core;
using com.baiba.core.ui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


namespace com.baiba.core.cliente
{
    public class GenerarOrden: MonoBehaviour
    {
        public int intLevel;
        public Orden ordenSelect;

        GameObject ui;
        Text txtOrden;
        Level level;
        List<Orden> ordenesLevel;


        private void Awake()
        {
            //string c = Constructor(1);
            //Guardar(c,intLevel.ToString());

            level = new Level();
            StreamReader reader = new StreamReader(CONST.RESOURCES.FILES_FOLDER + "Level " + intLevel + ".json");
            string json = reader.ReadToEnd();
            reader.Close();

            level = JsonUtility.FromJson<Level>(json);

            IngList.CargarLista(level);
            ordenesLevel = new List<Orden>();
                           
            foreach (Orden o in level.ordenes)
            {
                ordenesLevel.Add(o);
            }
                

            int x = UnityEngine.Random.Range(0, ordenesLevel.Count);
            ordenSelect = ordenesLevel[x];
            //Debug.Log(ordenesLevel[x].ingredientes[0].key + " " + ordenesLevel[x].ingredientes[1].key + " " + ordenesLevel[x].ingredientes[2].key);

            //MostrarOrden();
        }

        public void MostrarOrden()
        {
            ui = GameObject.FindGameObjectWithTag(CONST.TAG.UI);
            foreach (GameObject o in ui.GetComponent<UIController>().OrdenesUI)
            {
                if(!o.activeSelf)
                {
                    txtOrden = o.GetComponent<Text>();
                }
            }    


            foreach (Ingrediente ing in ordenSelect.ingredientes)
            {
                txtOrden.text += ing.key + "\n";
            }
            txtOrden.gameObject.SetActive(true);
        }

        public void OcultarOrden()
        {
            txtOrden.gameObject.SetActive(false);
        }

        private void ComprobarOrden(Orden orden)
        {
            if (orden == ordenSelect)
            {
                Debug.Log("Correcto");
            }
            else
            {
                Debug.Log("Sos Boludooo??");
            }
        }

        private string Constructor(int lvl)
        {
            Level level = new Level();
            level.level = "Level " + lvl.ToString();
            Orden orden1 = new Orden();
            Orden orden2 = new Orden();
            Orden orden3 = new Orden();

            orden1.key = "Orden 1";
            orden1.ingredientes = new Ingrediente[3];
            orden1.tiempoEspera = 10f;
            orden2.key = "Orden 2";
            orden2.ingredientes = new Ingrediente[3];
            orden2.tiempoEspera = 10f;
            orden3.key = "Orden 3";
            orden3.ingredientes = new Ingrediente[3];
            orden3.tiempoEspera = 10f;


            for (int i = 1; i < orden1.ingredientes.Length + 1; i++)
            {
                orden1.ingredientes[i - 1] = new Ingrediente();
                orden1.ingredientes[i - 1].key = i.ToString();
                orden1.ingredientes[i - 1].tiempo = 3f;
                orden1.ingredientes[i - 1].extra = "dfkodfnma";
            }

            for (int i = 4; i < orden2.ingredientes.Length + 4; i++)
            {
                orden2.ingredientes[i - 4] = new Ingrediente();
                orden2.ingredientes[i - 4].key = i.ToString();
                orden2.ingredientes[i - 4].tiempo = 3f;
                orden2.ingredientes[i - 4].extra = "dfkodfnma";
            }

            for (int i = 7; i < orden3.ingredientes.Length + 7; i++)
            {
                orden3.ingredientes[i - 7] = new Ingrediente();
                orden3.ingredientes[i - 7].key = i.ToString();
                orden3.ingredientes[i - 7].tiempo = 3f;
                orden3.ingredientes[i - 7].extra = "dfkodfnma";
            }

            level.ordenes = new Orden[3];
            level.ordenes[0] = orden1;
            level.ordenes[1] = orden2;
            level.ordenes[2] = orden3;

            Debug.Log(JsonUtility.ToJson(level));

            string aux = JsonUtility.ToJson(level);
            return aux; 
        }

        public void Guardar(string json, string level)
        {
            StreamWriter writer = new StreamWriter(CONST.RESOURCES.FILES_FOLDER + "Level " + level + ".json");
            Debug.Log("Json Guardado");
            writer.Write(json);
            writer.Close();
        }

    }

    [Serializable]
    public class Level
    {
        public string level;
        public Orden[] ordenes;
    }

    [Serializable]
    public class Orden
    {
        public string key;
        public Ingrediente[] ingredientes;
        public float tiempoEspera;
    }

    [Serializable]
    public class Ingrediente
    {
        public string key;
        public float tiempo;
        public string extra;
    }

    public static class IngList
    {
        public static List<Ingrediente> ingList = new List<Ingrediente>();

        public static void CargarLista(Level level)
        {
            foreach (Orden o in level.ordenes)
            {
                foreach (Ingrediente ing in o.ingredientes)
                {
                    if (!ingList.Contains(ing))
                        ingList.Add(ing);
                }
            }

            string str = "Lista de Ingredientes Cargados:\n";
            for (int i = 0; i < ingList.Count; i++)
            {
                str += ingList[i].key + "\n";
            }
            Debug.Log(str);

        }
    }

}



