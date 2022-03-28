using com.baiba.core;
using com.baiba.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.baiba.cliente
{
    using com.baiba.GameManager;
    public class ClienteScript: MonoBehaviour
    {
        public float velocidad;
        public float tiempoEspera;
        public Image temporizador;
        public Transform punto1;
        public Transform punto2;
        public Transform pared;

        private Transform destino;
        private Animator animator;
        private Orden orden;


        Transform t;

        private void Awake()
        {
            t = this.gameObject.transform;
            destino = punto1;
            animator = gameObject.GetComponent<Animator>();
        }

        private void Start()
        {   orden = GameManager.Nivel.ordenes[Random.Range(0, GameManager.Nivel.ordenes.Length - 1)];
            GameManager.ListaOrdenes.Add(this.gameObject,orden);
            tiempoEspera = orden.tiempo;            
            StartCoroutine(PedirOrden());
        }

        private void Update()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(CONST.TAG.PUNTODESAPARICION))
            {
                StopCoroutine(PedirOrden());
                GameManager.OrdenesPerdidas = +1;
                this.gameObject.SetActive(false);
            }
        }

        private IEnumerator PedirOrden()
        {
            if (Vector3.Distance(t.position, destino.position) > 0.25f)
            {
                t.LookAt(destino);
                t.Translate(Vector3.forward * velocidad * Time.deltaTime);
                animator.SetBool("Walk", true);

            }
            else
            {
                GameManager.MostrarOrden(this.gameObject);
                animator.SetBool("Walk", false);
                destino = punto2;
                temporizador.gameObject.SetActive(true);
                StartCoroutine(Temporizador(tiempoEspera));
                yield return new WaitForSeconds(tiempoEspera);
                

            }
            yield return new WaitForEndOfFrame();
            StartCoroutine(PedirOrden());            
        }

        private IEnumerator Temporizador(float tiempo)
        {
            if (tiempo > 0)
            {
                tiempo -= Time.deltaTime;
                temporizador.fillAmount = (tiempo/tiempoEspera);
                if((tiempo*100/tiempoEspera) <=50 & (tiempo * 100 / tiempoEspera) > 25)
                {
                    temporizador.color = new Color(0.93f, 0.49f, 0.1f);
                }
                else if((tiempo * 100 / tiempoEspera) <= 25)
                {
                    temporizador.color = Color.red;
                }

            }
            yield return new WaitForEndOfFrame();
            if (tiempo > 0)
            {
                StartCoroutine(Temporizador(tiempo));
            }
            else
            {
                temporizador.gameObject.SetActive(false);
            }
        }
    }
}

