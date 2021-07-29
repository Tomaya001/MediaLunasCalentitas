using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResaltarObjectScript : MonoBehaviour
{
    private MeshFilter[] meshes;
    private CombineInstance[] combineInstances;
    private Mesh combined;
    public Color color;
    private GameObject outline;
    // Start is called before the first frame update
    void Start()
    {
        meshes = GetComponentsInChildren<MeshFilter>();
        combineInstances = new CombineInstance[meshes.Length];
        int i = 0;
        while(i < meshes.Length)
        {
            combineInstances[i].mesh = meshes[i].sharedMesh;
            combineInstances[i].transform = meshes[i].transform.localToWorldMatrix;
            i++;
        }
        /*combined.CombineMeshes(combineInstances);
        outline = new GameObject();
        outline.AddComponent<MeshFilter>();*/
        outline = new GameObject();
        outline.AddComponent<MeshFilter>();
        outline.GetComponent<MeshFilter>().mesh.CombineMeshes(combineInstances);
        outline.AddComponent<MeshRenderer>();
        outline.AddComponent<MeshCollider>();
        outline.GetComponent<MeshRenderer>().material.color = color;
        outline.GetComponent<MeshCollider>().convex = true;
        outline.GetComponent<MeshCollider>().isTrigger = true;
        //outline.transform.localScale = this.transform.localScale * 1.01f;
        //outline.GetComponent<Material>().color = color;
        outline.transform.SetParent(this.transform);
        outline.SetActive(false);


    }

    public void Resaltar()
    {
        outline.SetActive(true);
    }

}
