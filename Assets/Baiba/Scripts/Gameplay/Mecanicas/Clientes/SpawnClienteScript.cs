using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baiba.core.cliente
{
    public class SpawnClienteScript : MonoBehaviour
    {
        public int cantCliente;
        public int tiempo;
        public static List<GameObject> poolClientes;

        public List<Text> ordenesUI;

        private GameObject prefab;
        private Transform zonaPedido;
        private Transform salida;
        private int idCliente;

        
        // Start is called before the first frame update
        void Start()
        {            
            idCliente = 0;
            GenerarPool();
            StartCoroutine(ControlClientes(tiempo));
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void GenerarPool()
        {
            poolClientes = new List<GameObject>();
            prefab = AssetLoader.GetAsset<GameObject>("prefab/Cliente");
            zonaPedido = GameObject.Find("Zona de Pedido").transform;
            salida = GameObject.Find("Salida").transform;

            for (int i = 0; i < cantCliente; i++)
            {
                GameObject aux = Instantiate<GameObject>(prefab);
                aux.GetComponent<ClienteMovimientoScript>().destino = zonaPedido;
                aux.GetComponent<ClienteMovimientoScript>().salida = salida;
                aux.GetComponent<ClienteMovimientoScript>().tiempo = aux.GetComponent<GenerarOrden>().ordenSelect.tiempoEspera;
                aux.transform.position = this.transform.position;
                poolClientes.Add(aux);
            }

            foreach (GameObject o in poolClientes)
            {
                o.gameObject.SetActive(false);
            }
        }

        IEnumerator ControlClientes(float tiempo)
        {   
            if (idCliente < poolClientes.Count)
            {
                poolClientes[idCliente].SetActive(true);
                poolClientes[idCliente].GetComponent<GenerarOrden>().MostrarOrden();
                idCliente++;
                yield return new WaitForSeconds(tiempo);
                StartCoroutine(ControlClientes(tiempo));
            }            
        }
    }
}
