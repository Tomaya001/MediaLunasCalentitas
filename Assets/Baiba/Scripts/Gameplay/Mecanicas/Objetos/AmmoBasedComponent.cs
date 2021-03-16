using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBasedComponent : GenericObject
{
    public GameObject prefab;    
    public Transform[] points;
    public int ammo;
    public bool vacio;

    private List<GameObject> pool;

    private void Start()
    {
        isPickable = false;
        PoolCreator();
        Rellenar();
    }

    private void Update()
    {
        if(transform.childCount<= points.Length)
        {
            vacio = true;
        }
    }

    public void Rellenar()
    {
        for (int i = 0; i < ammo; i++)
        {
            for (int f = 0; f < pool.Count; f++)
            {
                if (!pool[f].activeSelf)
                {
                    pool[f].transform.SetParent(transform);
                    pool[f].transform.position = points[i].transform.position;
                    pool[f].SetActive(true);
                    break;
                }
            }
        }
        vacio = false;
    }

    public Transform Sacar()
    {
        Transform _t = null;
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            if(transform.GetChild(i))
            {
                if (transform.GetChild(i).GetComponent<GenericObject>())
                {
                    //transform.GetChild(i).gameObject.tag = CONST.TAG.OBJETO;
                    _t = transform.GetChild(i).transform;
                    return _t;
                }
            }                              
        }
        return _t;
    }

    private void PoolCreator()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < ammo * 2; i++)
        {
            pool.Add(Instantiate(prefab));
            pool[i].gameObject.name = "Municion" + i;
        }

        foreach (GameObject g in pool)
        {
            g.gameObject.SetActive(false);
        }
    }
}
