using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntregaScript : MonoBehaviour
{
    public List<ingListScript> pedidosList;
    public Text txtPedido;

    public Text Win;
    public Text Lose;

    ingListScript orden;
    Transform aux;

    private void Start()
    {
        txtPedido.text = null;
        orden = pedidosList[Random.Range(0, pedidosList.Count)];
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
            Debug.Log("Victoria");
            new WaitForSeconds(3.0f);
            SceneManager.LoadScene("Pruebas");
        }
        else
        {
            Debug.Log("Derrota");
            new WaitForSeconds(3.0f);
            SceneManager.LoadScene("Pruebas");
        }
    }
}