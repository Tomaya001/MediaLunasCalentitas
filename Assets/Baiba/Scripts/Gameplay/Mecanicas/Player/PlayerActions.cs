using com.baiba.core;
using com.baiba.core.objeto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour
{
    public static Transform t;
    public Transform tI;
    public bool enabledCollisonTarget;
    public static bool canActionPlayer;
    public bool canActionPlayerI;
    public static bool resaltando;
    public bool resaltandoI;
    public static bool entrega;
    public bool entregaI;


    private Transform myT;

    private void Start()
    {
        myT = this.transform;
        canActionPlayer = false;
    }

    private void Update()
    {
        tI = t;
        canActionPlayerI = canActionPlayer;
        resaltandoI = resaltando;
        entregaI = entrega;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            t = other.gameObject.transform;
            if (t.Find("Resaltador") && !resaltando)
            {
                t.Find("Resaltador").gameObject.SetActive(true);
                resaltando = true;
            }
            enabledCollisonTarget = t.gameObject.GetComponent<Rigidbody>().detectCollisions;
            canActionPlayer = true;
        }
        else if (other.gameObject.CompareTag(CONST.TAG.PUNTOENTREGA))
        {
            t = other.gameObject.transform;
            canActionPlayer = false;
            entrega = true;

        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        canActionPlayer = false;
        entrega = false;
        resaltando = false;
        t = null;
    }

}
