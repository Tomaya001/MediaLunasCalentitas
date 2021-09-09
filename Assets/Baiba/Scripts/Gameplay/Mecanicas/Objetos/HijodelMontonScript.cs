using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.objeto
{
    public class HijodelMontonScript : MontonObjects
    {
        private Transform tPadre;

        private void Start()
        {
            tPadre = this.transform.parent;
        }
        public Transform Sacar()
        {
            Transform _t = this.transform;
            return _t;
        }

        /*public void EstadoInicio()
        {
            this.gameObject.SetActive(false);
            for (int i = 0; i < possInicial.Count; i++)
            {
                foreach (GameObject o in pool)
                {
                    if(o.transform.position != possInicial[i])
                    {
                        this.transform.position = possInicial[i];
                        this.transform.SetParent(tPadre);
                    }
                }
            }
        }*/
    }
}

