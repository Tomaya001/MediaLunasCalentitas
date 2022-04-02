using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacarPorcionScript : GenericObject
{
    public GameObject porcion;
    public List<GameObject> poolPorciones;

    private void Start()
    {

        for (int i = 0; i < 5; i++)
        {
            poolPorciones.Add(Instantiate(porcion));
        }

        foreach (GameObject t in poolPorciones)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void SacarPorcion(Transform point,bool parent)
    {
        foreach (GameObject t in poolPorciones)
        {
            if(t.gameObject.activeSelf == false)
            {
                t.GetComponent<Rigidbody>().detectCollisions = false;
                t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                if (parent)
                    t.transform.SetParent(point.parent);
                else
                    t.transform.SetParent(point);
                t.transform.position = point.position;
                
                t.SetActive(true);
                break;
            }
        }
    }

    /*public Transform SacarPorcion()
    {
        foreach (Transform t in poolPorciones)
        {
            if (!t.gameObject.activeSelf)
            {
                Debug.Log("Entree");
                t.gameObject.SetActive(true);
                return t;
            }
            else
                return null;            
        }
        Debug.Log("Null");
        return null;
    }*/
}
