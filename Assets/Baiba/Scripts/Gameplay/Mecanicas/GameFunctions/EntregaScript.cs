using com.baiba.core;
using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntregaScript : MonoBehaviour
{
    public List<ingListScript> pedidosList;
    public Text txtPedido;
    public int ordenesForNextLevel;

    public Text Win;
    public Text Lose;

    ingListScript orden;
    List<ingListScript> ordenesEntregadas;
    Transform aux;

    private void Awake()
    {
        if(GameManager.OrdenesCountLevel - 1 <= 0)
            GameManager.OrdenesCountLevel = ordenesForNextLevel;
    }

    private void Start()
    {
        txtPedido.text = null;
        ordenesEntregadas = new List<ingListScript>();
        randomOrden();
        for (int i = 0; i < orden.ingList.Count; i++)
        {
            txtPedido.text += orden.ingList[i] + "\n";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONST.TAG.PLAYER))
        {
            if(other.gameObject.GetComponentInChildren<Bandeja>())
            {
                aux = other.GetComponentInChildren<Bandeja>().gameObject.transform;
                ComprobarVictoria(ComprobarOrden(RecorrerBandeja(aux), orden));
            }
            else if (other.gameObject.GetComponentInChildren<PickUp>())
            {
                aux = other.GetComponentInChildren<PickUp>().gameObject.transform;
                ComprobarVictoria(ComprobarOrden(RecorrerBandeja(aux), orden));
            }
            
        }
    }

    List<string> RecorrerBandeja(Transform t)
    {
        List<string> aux = new List<string>();
        for (int i = 0; i < t.childCount; i++)
        {
            if(t.GetChild(i).gameObject.GetComponent<GenericObject>())
                aux.Add(t.GetChild(i).gameObject.GetComponent<GenericObject>().id);
        }
        return aux;
    }

    bool ComprobarOrden(List<string> bandeja,ingListScript orden)
    {
        bool aux = true;
        if(bandeja.Count != orden.ingList.Count)
        {
            aux = false;
            return aux;
        }
        for (int i = 0; i < bandeja.Count; i++)
        {
            if(bandeja[i] != orden.ingList[i])
            {
                aux = false;
                return aux;
            }
        }
        return aux;
    }

    void ComprobarVictoria(bool estado)
    {
        if(estado)
        {
            if (GameManager.OrdenesCountLevel > 0)
            {
                ordenesEntregadas.Add(orden);
                randomOrden();
                GameManager.OrdenesCountLevel--;
                SceneManager.LoadScene("Pruebas");
            }
            else
            {
                Debug.Log("Victoria");
                new WaitForSeconds(3.0f);
                SceneManager.LoadScene("Pruebas");
            }
            
        }
        else
        {
            Debug.Log("Derrota");
            new WaitForSeconds(3.0f);
            SceneManager.LoadScene("Pruebas");
        }
    }

    void randomOrden()
    {
        orden = pedidosList[Random.Range(0, pedidosList.Count)];
        while (ordenesEntregadas.Contains(orden))
        {
            orden = pedidosList[Random.Range(0, pedidosList.Count)];
        }
    }
}