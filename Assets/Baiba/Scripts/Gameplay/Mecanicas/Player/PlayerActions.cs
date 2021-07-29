using com.baiba.core;
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
    public static bool resaltandoI;
    public bool resaltando;


    private void Start()
    {
        canActionPlayer = false;
    }

    private void Update()
    {
        tI = t;
        canActionPlayerI = canActionPlayer;
        resaltandoI = resaltando;
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
    }
    
    public void OnTriggerExit(Collider other)
    {
        canActionPlayer = false;
        resaltando = false;
        t = null;
    }

}
