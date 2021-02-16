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

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.OBJETO))
        {
            t = other.gameObject.transform;
            canActionPlayer = true;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        canActionPlayer = false;
        t = null;
    }

}
