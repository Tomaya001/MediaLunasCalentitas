using com.baiba.core;
using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class EntregaScript : MonoBehaviour
{


    private Level level;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.CargarUIPrincipal();
        
    }

    public void ComprobarOrden(Transform t)
    {
        bool incorrecto = false;
        if(t.childCount == 0)
        {
            foreach (Orden o in GameManager.ListaOrdenes.Values)
            {
                if (o.ingredientes.Length == 1)
                {
                    if (t.gameObject.GetComponent<GenericObject>().id == o.ingredientes[0].nombre)
                    {
                        t.SetParent(null);
                        t.gameObject.SetActive(false);
                        Debug.Log("Correcto");
                        return;
                    }
                }
                else
                {
                    Debug.Log("No hay ninguna orden con un solo Ingrediente");
                }                    
            }
            incorrecto = true;
        }
        else if(t.childCount > 0)
        {
            List<string> aux = new List<string>();

            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).GetComponent<GenericObject>())
                    aux.Add(t.GetChild(i).gameObject.GetComponent<GenericObject>().id);
            }

            foreach (Orden o in GameManager.ListaOrdenes.Values)
            {
                if (aux.Count == o.ingredientes.Length)
                {
                    int i = 0;
                    bool correcto = true;
                    while (i < o.ingredientes.Length)
                    {
                        if (aux[i] == o.ingredientes[i].nombre)
                        {
                            i++;
                        }
                        else
                        {
                            correcto = false;
                            break;
                        }
                    }

                    if (correcto)
                    {
                        t.SetParent(null);
                        t.gameObject.SetActive(false);
                        Debug.Log("Correcto");
                        return;
                    }
                }
            }
            incorrecto = true;
        }

        if(incorrecto)
        {
            Debug.Log("Orden Erronea");
        }        
    }




    private void CrearJson()
    {
        Orden orden1 = new Orden();
        Orden orden2 = new Orden();
        Orden orden3 = new Orden();

        Ingrediente ing1 = new Ingrediente(1);
        Ingrediente ing2 = new Ingrediente(2);
        Ingrediente ing3 = new Ingrediente(3);

        orden1.ingredientes = new Ingrediente[3];
        orden1.ingredientes[0] = ing1;
        orden1.ingredientes[1] = ing2;
        orden1.ingredientes[2] = ing3;
        orden1.tiempo = 10f;

        orden2.ingredientes = new Ingrediente[3];
        orden2.ingredientes[0] = ing1;
        orden2.ingredientes[1] = ing2;
        orden2.ingredientes[2] = ing3;
        orden2.tiempo = 10f;

        orden3.ingredientes = new Ingrediente[3];
        orden3.ingredientes[0] = ing1;
        orden3.ingredientes[1] = ing2;
        orden3.ingredientes[2] = ing3;
        orden3.tiempo = 10f;

        Level level1 = new Level();
        Level level2 = new Level();

        level1.ordenes = new Orden[3];
        level1.ordenes[0] = orden1;
        level1.ordenes[1] = orden2;
        level1.ordenes[2] = orden3;

        level2.ordenes = new Orden[3];
        level2.ordenes[0] = orden1;
        level2.ordenes[1] = orden2;
        level2.ordenes[2] = orden3;

        string aux = JsonUtility.ToJson(level1);
        Debug.Log(aux);
    }

}

[Serializable]
public class Level
{
    public Orden[] ordenes;
}

[Serializable]
public class Orden
{
    public float tiempo;
    public Ingrediente[] ingredientes;
}

[Serializable]
public class Ingrediente
{
    public string nombre;
    public float tiempo;

    public Ingrediente(int i)
    {
        nombre = i.ToString();
        tiempo = 1.0f;
    }
}