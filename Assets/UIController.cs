using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baiba.core.ui
{
    public class UIController : MonoBehaviour
    {
        public List<GameObject> OrdenesUI;
        // Start is called before the first frame update
        void Start()
        {
            CargarOrdenesUI();
        }

        // Este Procedimiento busca los txtOrdenes y los carga a una lista publica para tener acceso a sus propiedades
        private void CargarOrdenesUI()
        {
            int aux = 1;
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name == "txtOrden " + aux)
                {
                    OrdenesUI.Add(this.transform.GetChild(i).gameObject);
                    aux++;
                }
            }

            foreach (GameObject o in OrdenesUI)
            {
                o.SetActive(false);
            }
        }
    }
}

