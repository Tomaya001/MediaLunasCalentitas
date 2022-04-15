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
    public Text botonAccion; // Cambiar por Imagen


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
            else if (other.gameObject.GetComponent<Outline>())
            {
                other.gameObject.GetComponent<Outline>().enabled = estado;
            }
            else if (other.gameObject.GetComponentInParent<Outline>())
            {
                other.transform.parent.gameObject.GetComponent<Outline>().enabled = estado;
            }
            return;
        }
        catch
        {
            
        }
        
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            if (t==null)
            {
                EstadoOutline(other, true);
                t = other.gameObject.transform;
                Debug.Log(t.gameObject.name);
                enabledCollisonTarget = t.gameObject.GetComponent<Rigidbody>().detectCollisions;
                canActionPlayer = true;
            }            
        }
    }*/

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            if (t == null)
            {
                EstadoOutline(other, true);
                t = other.gameObject.transform;
                ReferenciarUI(true);
                enabledCollisonTarget = t.gameObject.GetComponent<Rigidbody>().detectCollisions;
                canActionPlayer = true;
            }
        }

    }

    public void ReferenciarUI(bool activar)
    {
        if (activar)
        {
            try
            {
                botonAccion.text = t.gameObject.GetComponent<GenericObject>().id;
            }
            catch
            {

            }
        }
        else
        {
            try
            {
                botonAccion.text = null;
            }
            catch
            {

            }
        }
        
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            EstadoOutline(other, false);
            ReferenciarUI(false);
        }
        canActionPlayer = false;
        t = null;

    }

}
