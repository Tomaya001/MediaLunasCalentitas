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
    public Image botonAccion;
    public List<Sprite> iconos;


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

    /* private void OnTriggerStay(Collider other)
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

    public void OnTriggerEnter(Collider other)
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
        string aux;
        if (activar)
        {
            try
            {
                aux = t.gameObject.GetComponent<GenericObject>().id;
                botonAccion.sprite = GameManager.BuscarIcono(aux, iconos);
                if(botonAccion.sprite == null)
                    botonAccion.color = new Color(1f, 1f, 1f, 0f);
                else
                    botonAccion.color = new Color(1f, 1f, 1f, 1f);
            }
            catch
            {

            }
        }
        else
        {
            try
            {
                botonAccion.color = new Color(1f, 1f, 1f, 0f);
                botonAccion.sprite = null;
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
