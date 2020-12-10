using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public bool isPickable;
    public bool canCatch;
    public bool isCaught;

    Transform t;

    private void Start()
    {
        t = gameObject.transform;
    }
}
