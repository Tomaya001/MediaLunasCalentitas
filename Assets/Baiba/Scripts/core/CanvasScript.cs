using com.baiba.core;
using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.baiba.UI
{
    public class CanvasScript : MonoBehaviour
    {
        public Canvas canvasCliente;

        // Start is called before the first frame update
        void Start()
        {
            if(canvasCliente == null)
            {
                canvasCliente = this.gameObject.GetComponent<Canvas>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (canvasCliente != null)
                canvasCliente.transform.LookAt(Camera.main.transform);
        }
    }
}

