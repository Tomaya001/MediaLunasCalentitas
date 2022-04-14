using com.baiba.core;
using com.baiba.cliente;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanClientesScript : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] points;
    public float tiempoEspera;

    private float tiempo;
    private List<GameObject> poolClientes;
    public List<GameObject> PoolClientes
    {
        get { return poolClientes; }
        set { poolClientes = value; }
    }

    private void Awake()
    {
        poolClientes = new List<GameObject>();        
        GenerarPool();
    }

    private void Start()
    {
        tiempo = tiempoEspera;
        poolClientes[0].SetActive(true);
    }

    public void GenerarPool()
    {
        prefab.gameObject.tag = CONST.TAG.CLIENTE;
        for (int i = 0; i < 4; i++)
        {
            prefab.gameObject.name = "ClientePool " + i;
            Instantiate(prefab);
        }
        poolClientes.AddRange(GameObject.FindGameObjectsWithTag(CONST.TAG.CLIENTE));

        foreach (GameObject g in poolClientes)
        {
            g.transform.position = this.gameObject.transform.position;
            g.gameObject.GetComponent<ClienteScript>().punto1 = points[0].transform;
            g.gameObject.GetComponent<ClienteScript>().punto2 = points[1].transform;
            g.gameObject.GetComponent<ClienteScript>().pared = points[2].transform;
            g.gameObject.GetComponent<ClienteScript>().spwan = points[3].transform;
            g.SetActive(false);
        }
    }

    private void Update()
    {
        if(tiempo < 0f)
        {
            foreach(GameObject g in poolClientes)
            {
                if (!g.activeSelf)
                {
                    g.SetActive(true);
                    break;
                }
            }
            tiempo = tiempoEspera;
        }
        tiempo -= Time.deltaTime;
    }

}
