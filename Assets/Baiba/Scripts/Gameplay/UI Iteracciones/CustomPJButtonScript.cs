using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPJButtonScript : MonoBehaviour
{
    public GameObject cabeza;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarColor()
    {
        Debug.Log(cabeza.GetComponent<Renderer>().material.name);
        //cabeza.GetComponent<Material>().color = Color.red;
    }


}
