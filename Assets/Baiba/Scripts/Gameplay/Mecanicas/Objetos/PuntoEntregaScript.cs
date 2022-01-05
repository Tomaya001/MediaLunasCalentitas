using com.baiba.core.cliente;
using com.baiba.core.objeto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoEntregaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool Comprobar(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            foreach (GameObject c in SpawnClienteScript.poolClientes)
            {
                if(c.activeSelf == true)
                {
                    foreach (Ingrediente ing in c.GetComponent<GenerarOrden>().ordenSelect.ingredientes)
                    {
                        if (ing.key != t.GetChild(i).GetComponent<GenericObject>().id)
                        {
                            return false;
                        }
                    }
                }                
            }
        }
        return true;
    }
}
