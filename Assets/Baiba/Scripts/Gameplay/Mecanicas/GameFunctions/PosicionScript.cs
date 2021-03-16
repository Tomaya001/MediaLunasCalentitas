
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionScript : MonoBehaviour
{
    private static Dictionary<string, Vector3> transList;
    public static Dictionary<string, Vector3> TransList
    {
        get { return transList; }
    }
    // Start is called before the first frame update
    void Start()
    {
        transList = new Dictionary<string, Vector3>();
        SetTransformLists();
    }

    public static void SetTransformLists()
    {
        transList.Clear();
        for (int i = 0; i < FindObjectsOfType<GenericObject>().Length; i++)
        {
            transList.Add(FindObjectsOfType<GenericObject>()[i].name,
                FindObjectsOfType<GenericObject>()[i].transform.position);
        }
    }
}
