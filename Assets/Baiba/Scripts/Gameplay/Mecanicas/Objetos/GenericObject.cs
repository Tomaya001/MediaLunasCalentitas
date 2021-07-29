using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericObject : MonoBehaviour
{
    public string id;
    public bool isPickable;
    public bool isTrash;


    // Declaramos las variables que vamos a usar para crear el mesh a partir de los objetos hijos del gameobject
    private MeshFilter[] meshFilters;
    private CombineInstance[] combines;
    public Material material;
    GameObject aux;

    private void Start()
    {
        if (transform.childCount != 0)
            GenerarMesh(transform.GetChild(0));
        else
            CopiarMesh(this.transform);

        
    }

    protected void GenerarMesh(Transform t)
    {
        try
        {
            meshFilters = t.gameObject.GetComponentsInChildren<MeshFilter>();
            combines = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < combines.Length)
            {
                combines[i].mesh = meshFilters[i].sharedMesh;
                combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
                i++;
            }
            aux = new GameObject();
            aux.name = "Resaltador";
            aux.gameObject.AddComponent<MeshFilter>();
            aux.gameObject.AddComponent<MeshRenderer>();
            aux.GetComponent<MeshFilter>().mesh = new Mesh();
            aux.GetComponent<MeshFilter>().mesh.CombineMeshes(combines);
            aux.GetComponent<MeshRenderer>().material = material;
            aux.transform.SetParent(this.transform);
            aux.SetActive(false);

        }
        catch (System.Exception)
        {
            Debug.Log(t.gameObject.name + " No tiene Mesh en los hijos");
            throw;
        }
        
    }

    protected void CopiarMesh(Transform t)
    {
        aux = new GameObject();
        aux.name = "Resaltador";
        aux.gameObject.AddComponent<MeshFilter>();
        aux.gameObject.AddComponent<MeshRenderer>();
        aux.GetComponent<MeshFilter>().mesh = t.gameObject.GetComponent<MeshFilter>().sharedMesh;
        aux.GetComponent<MeshRenderer>().material = material;
        aux.transform.SetParent(t);
        aux.transform.localPosition = Vector3.zero;
        aux.SetActive(false);
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(CONST.TAG.PLAYER))
        {
            aux.SetActive(true);
        }
    }*/

    protected void OnTriggerExit(Collider other)
    {
        if (this.gameObject.transform.Find("Resaltador"))
            aux.SetActive(false);
    }
}
