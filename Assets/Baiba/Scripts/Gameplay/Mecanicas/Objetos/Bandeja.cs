using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandeja : PickUp
{
    public bool inUse;
    public bool canbeReleased;

    private void Start()
    {
        i = 0;
        inUse = false;
        canbeReleased = false;
    }

    private void Update()
    {
        if (this.transform.parent)
            inUse = true;
        else
            inUse = false;

        if (t == null)
            canbeReleased = true;
        else
            canbeReleased = false;

    }

    public void Vaciar()
    {
        points = null;
    }

}
