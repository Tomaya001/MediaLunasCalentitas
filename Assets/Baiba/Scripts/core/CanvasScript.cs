using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.baiba.UI
{
    public class CanvasScript : MonoBehaviour
    {
        public Canvas canvas;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            canvas.transform.LookAt(Camera.main.transform);
        }
    }
}

