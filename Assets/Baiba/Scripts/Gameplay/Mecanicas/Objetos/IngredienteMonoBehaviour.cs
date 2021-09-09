using com.baiba.core.cliente;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteMonoBehaviour : GenericObject
{

    public float tiempo;
    void Start()
    {
        foreach (Ingrediente ing in IngList.ingList)
        {
            if (id == ing.key)
                tiempo = ing.tiempo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
