using com.baiba.core;
using com.baiba.cliente;
using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;
using UnityEngine.Audio;

public class EntregaScript : GenericObject
{
    private Level level;
    public AudioSource source;
    public AudioClip audioError;
    public AudioClip audioCorrecto;

    private void Start()
    {
        GameManager.CargarUIPrincipal();
        
    }

    public void ComprobarOrden(Transform t)
    {
        bool correcto = true;
        if(t.childCount == 1)
        {
            foreach (Orden o in GameManager.ListaOrdenes.Values)
            {
                if (o.ingredientes.Length == 1)
                {
                    if (t.gameObject.GetComponent<GenericObject>().id == o.ingredientes[0].nombre)
                    {
                        t.parent.GetComponentInParent<Animator>().SetBool("Pick", false);
                        t.SetParent(null);
                        t.gameObject.SetActive(false);
                        Debug.Log(GameManager.ListaOrdenes.Where(p => p.Value == o).FirstOrDefault().Key);
                        GameManager.Puntos += (o.ingredientes.Length * 25);
                        Debug.Log("Correcto");                        
                        GameManager.ListaOrdenes.Where(p => p.Value == o).FirstOrDefault().Key.gameObject.GetComponent<ClienteScript>().OrdenCompletada();
                        GameManager.Puntos += (o.ingredientes.Length * 25);
                        source.clip = audioCorrecto;
                        source.Play();
                        return;
                    }
                }
                else
                {
                    Debug.Log("No hay ninguna orden con un solo Ingrediente");
                }                    
            }
        }
        else if(t.childCount > 1)
        {
            List<string> stringIds = new List<string>();

            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).GetComponent<GenericObject>())
                    stringIds.Add(t.GetChild(i).gameObject.GetComponent<GenericObject>().id);
            }

            foreach (Orden o in GameManager.ListaOrdenes.Values)
            {
                correcto = true;
                for (int i = 0; i < o.ingredientes.Length; i++)
                {
                    if (!stringIds.Contains(o.ingredientes[i].nombre))
                    {
                        correcto = false;
                    }
                }

                if (correcto)
                {
                    t.GetComponentInParent<Animator>().SetBool("Pick", false);
                    t.SetParent(null);
                    if (t.gameObject.GetComponent<Bandeja>())
                    {
                        for (int f = 0; f < t.childCount; f++)
                        {
                            if (t.GetChild(f).gameObject.GetComponent<GenericObject>())
                            {
                                t.GetChild(f).gameObject.SetActive(false);
                                t.GetChild(f).SetParent(null);
                                f--;
                            }
                        }
                        t.SetParent(null);
                        t.transform.position = t.gameObject.GetComponent<Bandeja>().possInicial;
                        t.transform.rotation = t.gameObject.GetComponent<Bandeja>().rootInicial;
                        t.transform.GetComponent<Collider>().enabled = true;
                        t.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
                        t.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        t.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        t.gameObject.GetComponent<Bandeja>().Descopupar(t);
                    }
                    else
                    Debug.Log(GameManager.ListaOrdenes.Where(p => p.Value == o).FirstOrDefault().Key);
                    GameManager.ListaOrdenes.Where(p => p.Value == o).FirstOrDefault().Key.gameObject.GetComponent<ClienteScript>().OrdenCompletada();
                    GameManager.Puntos += (o.ingredientes.Length * 25);
                    Debug.Log("Correcto");
                    source.clip = audioCorrecto;
                    source.Play();
                    return;
                } 
            }

            if (!correcto)
            {
                Debug.Log("Orden Erronea, Limpie la bandeja");
                source.clip = audioError;
                source.Play();
            }

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