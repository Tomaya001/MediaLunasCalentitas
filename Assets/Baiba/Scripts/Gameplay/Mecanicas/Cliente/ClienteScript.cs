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
        public bool estado;

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
                if (!estado)
                {
                    GameManager.OrdenesPerdidas += 1;
                    Debug.Log("Ordenes Perdidas: " + GameManager.OrdenesPerdidas);
                }
                else
                {
                    GameManager.OrdenesCorrectas += 1;
                    Debug.Log("Ordenes Correctas: " + GameManager.OrdenesCorrectas);
                }
                
                this.gameObject.SetActive(false);
            }
        }

        public void OrdenCompletada()
        {
            StopAllCoroutines();
            StartCoroutine(OrdenCompletadaRutina());
        }

        private IEnumerator OrdenCompletadaRutina()
        {            
            Debug.Log("Soy Feliz");
            GameManager.OcultarOrden(this.gameObject);
            estado = true;
            t.LookAt(punto2);
            if (Vector3.Distance(t.position, punto2.position) > 0.25f)
            {
                t.Translate(Vector3.forward * velocidad * Time.deltaTime);
                animator.SetBool("Walk", true);
                yield return new WaitForEndOfFrame();
                StartCoroutine(OrdenCompletadaRutina());
            }
            else
            {
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
                estado = false;
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

