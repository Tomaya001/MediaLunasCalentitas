﻿using com.baiba.core;
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

    private void EstadoOutline(Collider other, bool estado)
    {
        try
        {
            if (other.gameObject.GetComponent<SacardelMontonScript>())
            {
                other.gameObject.GetComponent<SacardelMontonScript>().ResaltarTaza(estado);
            }
            if (other.gameObject.GetComponent<Outline>())
            {
                other.gameObject.GetComponent<Outline>().enabled = estado;
            }
            if (other.gameObject.GetComponentInParent<Outline>())
            {
                other.transform.parent.gameObject.GetComponent<Outline>().enabled = estado;
            }
        }
        catch
        {
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            EstadoOutline(other, true);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            t = other.gameObject.transform;
            enabledCollisonTarget = t.gameObject.GetComponent<Rigidbody>().detectCollisions;
            canActionPlayer = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            EstadoOutline(other, false);
        }
        canActionPlayer = false;
        t = null;
    }

}
