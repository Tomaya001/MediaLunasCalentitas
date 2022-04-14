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
        public Transform spwan;
        public bool estado;

        private Transform destino;
        private Animator animator;
        private Orden orden;
        private bool caminar;
        private bool pidio;

        private Coroutine temp;


        Transform t;

        private void Awake()
        {
            t = this.gameObject.transform;
            animator = gameObject.GetComponent<Animator>();
        }

        private void Start()
        {   
        }

        private void OnEnable()
        {
            orden = GameManager.Nivel.ordenes[Random.Range(0, GameManager.Nivel.ordenes.Length - 1)];
            if (GameManager.ListaOrdenes.ContainsKey(this.gameObject))
            {
                GameManager.ListaOrdenes.Remove(this.gameObject);
            }
            GameManager.ListaOrdenes.Add(this.gameObject, orden);
            tiempoEspera = orden.tiempo;
            destino = punto1;
            caminar = true;
            pidio = false;
        }

        private void Update()
        {
            if (caminar)
            {
                if (Vector3.Distance(t.position, destino.position) > 0.25f)
                {
                    t.LookAt(destino);
                    t.Translate(Vector3.forward * velocidad * Time.deltaTime);
                    animator.SetBool("Walk", true);
                }
                else
                {
                    if (!pidio)
                    {
                        PedirOrden();
                    }
                    caminar = false;
                }
            }
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(CONST.TAG.PUNTODESAPARICION))
            {
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
                this.transform.position = spwan.position;
                this.gameObject.SetActive(false);
            }
            else if(collision.gameObject.CompareTag(CONST.TAG.CLIENTE))
            {                
                caminar = false;
                destino = punto2;
                PedirOrden();
            }
        }        

        public void OrdenCompletada()
        {
            StopCoroutine(temp);
            Debug.Log("Soy Feliz");
            GameManager.OcultarOrden(this.gameObject);
            destino = punto2;
            estado = true;
            caminar = true;
        }

        private void PedirOrden()
        {
            pidio = true;
            t.LookAt(pared);
            GameManager.MostrarOrden(this.gameObject);
            temp = StartCoroutine(Temporizador(tiempoEspera));
        }

        private IEnumerator Temporizador(float tiempo)
        {
            temporizador.gameObject.SetActive(true);
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
                estado = false;
                caminar = true;
            }
        }
    }
}

