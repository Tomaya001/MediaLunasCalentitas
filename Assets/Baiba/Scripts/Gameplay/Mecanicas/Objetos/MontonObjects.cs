using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.objeto
{
    public class MontonObjects : GenericObject
    {
        public static List<GameObject> pool;
        protected List<Vector3> possInicial;
        protected Transform t;
        private BoxCollider myCollider;

        // Start is called before the first frame update
        void Start()
        {
            myCollider = this.gameObject.GetComponent<BoxCollider>();
            //myCollider.enabled = false;
            t = this.gameObject.transform;
            pool = new List<GameObject>();
            possInicial = new List<Vector3>();
            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.GetComponent<HijodelMontonScript>())
                {
                    pool.Add(t.GetChild(i).gameObject);
                    possInicial.Add(t.GetChild(i).localPosition);
                }
            }


            StartCoroutine(ComprobarHijos());
        }

        public void Rellenar()
        {
            int i = 0;
            while(pool.Count > t.childCount)
            {
                foreach (GameObject o in pool)
                {
                    if(o.transform.parent != t)
                    {
                        o.transform.SetParent(t);
                        o.transform.position = t.position;
                        o.transform.localPosition = possInicial[i];
                        o.SetActive(true);
                        i++;
                    }                    
                }
                
            }
        }

        private IEnumerator ComprobarHijos()
        {
            if (t.childCount == 0)
            {
                myCollider.enabled = true;
            }
            if(myCollider & t.childCount != 0)
            {
                myCollider.enabled = false;
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(ComprobarHijos());
        }


    }
}

