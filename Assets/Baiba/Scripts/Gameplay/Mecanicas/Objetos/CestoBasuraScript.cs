using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CestoBasuraScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CONST.TAG.PLAYER))
        {
            if (other.gameObject.GetComponentInChildren<Bandeja>())
            {
                other.gameObject.GetComponentInChildren<Bandeja>().Limpiar();
            }
        }
    }
}
