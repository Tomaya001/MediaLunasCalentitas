using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedComponent : GenericObject
{
    public float timeA;
    public float timeB;

    private Mesh mesh1;
    public Mesh mesh2;
    public Mesh mesh3;

    public bool action;



    private void Start()
    {
        action = false;
        isPickable = false;
        isTrash = false;
        mesh1 = this.gameObject.GetComponent<MeshFilter>().mesh;
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
            StopCoroutine(this.ActiveAction());
        }
        else
        {
            isTrash = true;
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh3;
            this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh3;
        }
        
    }

    public void ResetMesh(IEnumerator corrutina)
    {
        StopCoroutine(corrutina);
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh1;
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh1;
    }
}
