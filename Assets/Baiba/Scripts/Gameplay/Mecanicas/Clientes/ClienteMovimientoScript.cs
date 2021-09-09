using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.cliente
{
    public class ClienteMovimientoScript : MonoBehaviour
    {
        public Transform destino;
        public Transform salida;
        public bool happy;
        public float tiempo;

        float speed;
        Transform t;
        void Start()
        {
            t = this.transform;
            speed = 5f;
            StartCoroutine(Pedir(tiempo));
        }

        // Update is called once per frame


        private void Mover(Transform target)
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        IEnumerator Pedir(float time)
        {
            while ((t.position.x - destino.position.x) > 1f)
            {
                Mover(destino);
                yield return new WaitForEndOfFrame();
            }
            transform.LookAt(GameObject.FindGameObjectWithTag(CONST.TAG.MAINCAMERA).transform);
            yield return new WaitForSeconds(time);
            if (happy)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            while ((t.position.x - salida.position.x) > 1f)
            {
                Mover(salida);
                yield return new WaitForEndOfFrame();
            }
            this.gameObject.GetComponent<GenerarOrden>().OcultarOrden();
            this.gameObject.SetActive(false);
        }

    }

}
