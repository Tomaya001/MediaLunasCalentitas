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
    bool Winnn;

    private void Start()
    {
        Winnn = true;
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
            int aux = 0;
            for (int i = 0; i < other.transform.childCount ; i++)
            {
                if(other.transform.GetChild(i).GetComponent<GenericObject>())
                {
                    if(other.transform.GetChild(i).GetComponent<GenericObject>().id == orden.ingList[aux])
                    {
                        aux++;
                    }
                    else
                    {
                        Winnn = false;
                        break;
                    }
                }
            }
            if (Winnn)
            {
                Debug.Log("YOU ARE WIN");
                Time.timeScale = 0;
            }
            else
            {
                Debug.Log("YOU LOSE");
                Time.timeScale = 0;
            }
        }
    }
}