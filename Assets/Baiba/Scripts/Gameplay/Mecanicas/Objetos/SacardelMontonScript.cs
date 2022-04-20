using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacardelMontonScript : GenericObject
{
    // Creamos una Lista para almacenar los objetos hijos
    private List<GameObject> objects;
    private List<Vector3> possBases;
    private int señalador;

    private void Awake()
    {
        objects = new List<GameObject>();
        possBases = new List<Vector3>();
        señalador = 0;

        GenerarPool();
    }

    private void GenerarPool()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<GenericObject>())
            {
                objects.Add(transform.GetChild(i).gameObject);
                possBases.Add(transform.GetChild(i).position);
                transform.GetChild(i).gameObject.tag = CONST.TAG.UNTAGGED;
            }
        }
    }

    public void Sacar(Transform player, bool padre)
    {
        foreach (GameObject g in objects)
        {
            if (g.gameObject.GetComponent<GenericObject>())
            {
                if(g.transform.parent == this.transform)
                {
                    g.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    g.gameObject.GetComponent<Collider>().enabled = false;
                    g.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    g.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    g.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    g.gameObject.GetComponent<Outline>().enabled = false;
                    g.transform.position = player.position;
                    g.tag = CONST.TAG.OBJETO;
                    if (padre)
                        g.transform.SetParent(player.parent);
                    else
                        g.transform.SetParent(player);
                    señalador++;
                    return;
                }                
            }
        }

       /* for (int i = 0; i <= transform.childCount; i--)
        {
            if (transform.GetChild(i))
            {
                if (transform.GetChild(i).gameObject.GetComponent<GenericObject>())
                {
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    transform.GetChild(i).gameObject.GetComponent<Collider>().enabled = false;
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().useGravity = false;
                    transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    transform.GetChild(i).gameObject.GetComponent<Outline>().enabled = false;
                    transform.GetChild(i).position = player.position;
                    transform.GetChild(i).tag = CONST.TAG.OBJETO;
                    transform.GetChild(i).SetParent(player.parent);
                    señalador++;
                    return;
                }
            }
        }*/
    }

    public void Rellenar()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeSelf)
            {
                objects[i].transform.position = possBases[i];
                objects[i].transform.SetParent(this.transform);
                objects[i].SetActive(true);
            }            
        }
    }

    public void ResaltarTaza(bool resaltar)
    {
        if(transform.childCount != 0)
        {
            if (resaltar)
                transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = true;
            else
                transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        }
    }


}
