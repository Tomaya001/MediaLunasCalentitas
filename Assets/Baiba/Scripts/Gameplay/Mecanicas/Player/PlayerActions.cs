using com.baiba.core;
using com.baiba.GameManager;
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


    private void Start()
    {
        canActionPlayer = false;
    }

    private void Update()
    {
        tI = t;
        canActionPlayerI = canActionPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            other.gameObject.GetComponent<Outline>().enabled = true;
            
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            t = other.gameObject.transform;
            enabledCollisonTarget = t.gameObject.GetComponent<Rigidbody>().detectCollisions;
            canActionPlayer = true;
            GameManager.TextoBoton.GetComponent<Text>().text = other.gameObject.GetComponent<GenericObject>().id;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            other.gameObject.GetComponent<Outline>().enabled = false;
        }
        canActionPlayer = false;
        t = null;
        GameManager.TextoBoton.GetComponent<Text>().text =  "";
    }

}
