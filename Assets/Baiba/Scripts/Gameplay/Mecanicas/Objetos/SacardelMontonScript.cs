using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacardelMontonScript : GenericObject
{
    // Creamos una Lista para almacenar los objetos hijos
    private List<GameObject> objects;
    private List<Transform> possBases;
    

    void Start()
    {
        // Inicializacion de Variables
        objects = new List<GameObject>();
        possBases = new List<Transform>();


        // Con el for recorremos el objeto padre como si fuera un vector y por cada vuelta agregamos un hijo a la lista de object
        // y almacenamos la pocicion base del objeto */
        for (int i = 0; i < this.transform.childCount ; i++)
        {
            if(this.transform.GetChild(i).GetComponent<GenericObject>())
            {
                objects.Add(this.transform.GetChild(i).gameObject);
                possBases.Add(this.transform.GetChild(i));
                transform.GetChild(i).gameObject.tag = CONST.TAG.UNTAGGED;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTransformChildrenChanged()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = possBases[i].position;
        }
    }

    public Transform Sacar()
    {
        Transform _t = null;
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            if (transform.GetChild(i))
            {
                if (transform.GetChild(i).GetComponent<GenericObject>())
                {
                    transform.GetChild(i).gameObject.tag = CONST.TAG.OBJETO;
                    //transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                    _t = transform.GetChild(i).transform;
                    return _t;
                }
            }
        }
        return _t;
    }

    public void Rellenar()
    {
        
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
