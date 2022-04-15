using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedirOrden : MonoBehaviour
{
    private Orden orden;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManager.Nivel);
        orden = GameManager.Nivel.ordenes[Random.Range(0, GameManager.Nivel.ordenes.Length -1)];
        Debug.Log(orden);
        GameManager.ListaOrdenes.Add(this.gameObject,orden);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
