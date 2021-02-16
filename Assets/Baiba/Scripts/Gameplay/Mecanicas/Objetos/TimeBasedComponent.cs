using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedComponent : GenericObject
{
    public float timeA;
    public float timeB;

    public Mesh mesh2;
    public Mesh mesh3;

    public bool action;

    private void Start()
    {
        action = false;
        isPickable = false;
        isTrash = false;
    }

    public IEnumerator ActiveAction()
    {
        action = true;
        yield return new WaitForSeconds(timeA);
        isPickable = true;
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh2;
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh2;
        yield return new WaitForSeconds(timeB);
        if(this.gameObject.transform.parent)
        {
            yield return null;
        }
        else
        {
            isTrash = true;
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh3;
            this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh3;
        }
        
    }
}
