using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : PlayerActions
{    
    public Transform[] points;
    protected int i;

    private Transform thisT;
    public Animator animator;

    public string[] ids;

    private void Awake()
    {
        i = 0;
        thisT = this.transform;
        if (!this.gameObject.GetComponent<Bandeja>())
            animator = this.gameObject.transform.parent.GetComponentInChildren<Animator>();
    }

    protected int Pick(Transform[] _points, Transform p, int _i)
    {
        if(t.gameObject.GetComponent<GenericObject>().isPickable)
        {   
            if(t.gameObject.GetComponent<AmmoBasedComponent>())
            {
                if (t.gameObject.GetComponent<AmmoBasedComponent>().vacio)
                {
                    Debug.Log("Debe Rellenar la Bandeja");
                    return _i;
                }
            }

            int j = _i;

            while (j < _points.Length)
            {
                if (j + 1 >= _points.Length)
                {
                    animator.SetBool("Pick",true);
                    t.position = _points[_i].position;
                    if(t.GetComponent<Bandeja>())
                        t.rotation = _points[_i].rotation;
                    t.SetParent(p);
                    t.GetComponent<Collider>().enabled = false;
                    t.GetComponent<Rigidbody>().detectCollisions = false;
                    t.GetComponent<Rigidbody>().useGravity = false;
                    t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    t.GetComponent<Outline>().enabled = false;
                    _i++;
                    break;
                }
                j++;
            }
        }        
        return _i;
    }

    protected int Soltar(Transform p, int _i)
    {
        for (int j = p.childCount; j > 0 ; j--)
        {
            p.GetChild(j - 1).gameObject.GetComponent<Collider>().enabled = true;
            p.GetChild(j-1).gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            p.GetChild(j - 1).gameObject.GetComponent<Rigidbody>().useGravity = true;
            p.GetChild(j - 1).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            p.GetChild(j-1).SetParent(null);
            _i--;
        }
        animator.SetBool("Pick", false);
        return _i;
    }
    
    public void Limpiar()
    {
        int j = 0;
        while (thisT.childCount != 0 && j <= thisT.childCount)
        {
            if (thisT.GetChild(j).GetComponent<GenericObject>())
            {
                while (thisT.GetChild(j).GetComponent<GenericObject>())
                {
                    if (PosicionScript.TransList.ContainsKey(thisT.GetChild(j).gameObject.name))
                    {
                        thisT.GetChild(j).transform.position = PosicionScript.TransList[thisT.GetChild(j).gameObject.name];
                        thisT.GetChild(j).GetComponent<Rigidbody>().detectCollisions = true;
                        if (thisT.GetChild(j).GetComponent<TimeBasedComponent>())
                            thisT.GetChild(j).GetComponent<TimeBasedComponent>().ResetMesh(thisT.GetChild(j).GetComponent<TimeBasedComponent>().ActiveAction());
                        thisT.GetChild(j).SetParent(null);
                    }
                    if (thisT.childCount <= j)
                        break;
                }                    
            }
            j++;
        }
        i = 0;

       /* for (int j = 0; j < this.transform.childCount; j++)
        {
            while (this.transform.GetChild(j).GetComponent<GenericObject>())
            {
                if (PosicionScript.TransList.ContainsKey(this.transform.GetChild(j).gameObject.name))
                {
                    this.transform.GetChild(j).transform.position = PosicionScript.TransList[this.transform.GetChild(j).gameObject.name];
                }
                this.transform.GetChild(j).GetComponent<Rigidbody>().detectCollisions = true;
                this.transform.GetChild(j).SetParent(null);
            }

            /*if (this.transform.GetChild(j).GetComponent<GenericObject>())
            {
                if (PosicionScript.TransList.ContainsKey(this.transform.GetChild(j).gameObject.name))
                {
                    this.transform.GetChild(j).transform.position = PosicionScript.TransList[this.transform.GetChild(j).gameObject.name];
                }
                this.transform.GetChild(j).GetComponent<Rigidbody>().detectCollisions = true;
                this.transform.GetChild(j).SetParent(null);
                //this.transform.GetChild(j).gameObject.SetActive(false);
            }*/
        
    }


    public void ActionPlayer()
    {
        //Preguntamos si el jugador puede realizar una accion que se activa cuando el jugador esta colisionando con algun trigger
        if (canActionPlayer)
        {
            //Preguntamos si es un objeto que tenga que preparase primero, de ser asi activamos la corrutina para que el objeto se prepare
            if(t.gameObject.GetComponent<TimeBasedComponent>())
            {
                 if(!t.gameObject.GetComponent<TimeBasedComponent>().action)
                {
                    t.gameObject.GetComponent<TimeBasedComponent>().action = true;
                    StartCoroutine(t.gameObject.GetComponent<TimeBasedComponent>().ActiveAction());
                }
            }
            //Preguntamos si es un objeto con una cantida finita de municion, de ser asi comprobamos si esta vacio, en tal caso lo rellenamos
            else if(t.gameObject.GetComponent<AmmoBasedComponent>())
            {
                if (t.gameObject.GetComponent<AmmoBasedComponent>().Sacar() == null)
                {
                    t.gameObject.GetComponent<AmmoBasedComponent>().Rellenar();
                    return;
                }
                else
                {
                    t = t.gameObject.GetComponent<AmmoBasedComponent>().Sacar();
                }
                    
            }
            /* Preguntamos si es un grupo de objetos que tienen una cantidad finita de objetos, de ser asi comprobamos si esta vacio, de estarlo rellenamos, caso contrario
               cogemos un objeto*/
            else if (t.gameObject.GetComponent<SacardelMontonScript>())
            {
                if (t.gameObject.GetComponent<SacardelMontonScript>().Sacar() == null)
                {
                    t.gameObject.GetComponent<SacardelMontonScript>().Rellenar();
                    return;
                }
                else
                {
                    Pick(points, t.gameObject.GetComponent<SacardelMontonScript>().Sacar(), i);
                }

            }

            else if (t.gameObject.GetComponent<SacarPorcionScript>())
            {
                t.gameObject.GetComponent<SacarPorcionScript>().SacarPorcion(thisT);
                animator.SetBool("Pick", true);
                //Pick(points, t.gameObject.GetComponent<SacarPorcionScript>().SacarPorcion(), i);
            }

            /*------Cambiar Descrip*//*Preguntamos si dentro de su herencia de objeto se encuentra la bandeja, de ser asi al Procedimiento Pick le mandamos los puntos y el tranform de la bandeja para setearla como padre
            de los objetos*/
            else if (thisT.GetComponentInChildren<Bandeja>())
            {
                if (t.gameObject.GetComponent<GenericObject>())
                {
                    if (thisT.GetComponentInChildren<Bandeja>().points.Length > thisT.GetComponentInChildren<Bandeja>().i)
                    {
                        thisT.GetComponentInChildren<Bandeja>().i = Pick(thisT.GetComponentInChildren<Bandeja>().points, thisT.GetComponentInChildren<Bandeja>().transform,
                            thisT.GetComponentInChildren<Bandeja>().i);
                    }
                }
                else if(t.gameObject.GetComponent<EntregaScript>())
                {
                    t.gameObject.GetComponent<EntregaScript>().ComprobarOrden(thisT.GetChild(0));
                }
                
            }/*------Cambiar Descrip*//*Preguntamos si el personaje tiene una taza en la mano, de ser asi procedemos a preguntar si interactuamos con la cafetera*/
            else if(thisT.childCount != 0)
            {
                if (thisT.GetChild(0).GetComponent<GenericObject>().id == "Taza")
                {
                    if (t.gameObject.GetComponent<CafeteraScripts>())
                    {
                        t.gameObject.GetComponent<CafeteraScripts>().ActivarCafetera(thisT.GetChild(0));
                        animator.SetBool("Pick", false);
                    }
                }
                /*Preguntamos si estamos frente a la caja, de ser asi habilitamos la opcion de entregar la orden*/
                else if (t.gameObject.GetComponent<EntregaScript>())
                {                   
                    if (thisT.GetChild(0).GetComponent<GenericObject>())
                    {
                        t.gameObject.GetComponent<EntregaScript>().ComprobarOrden(thisT.GetChild(0));
                    }
                }
            }
            /*Preguntamos si estamos frente a la cafetera y si tiene una taza lista, en ese caso la tomamos de nuevo*/
            else if (t.gameObject.GetComponent<CafeteraScripts>())
            {
                bool action = t.gameObject.GetComponent<CafeteraScripts>().SacarTaza(thisT);
                if (action)
                animator.SetBool("Pick", true);
            }

            /*De no tener la bandeja le pasamos los datos de la mano del player*/
            else if(t.gameObject.GetComponent<GenericObject>())
            {
                i = Pick(points, thisT, i);
            }                
        }
        //De no poder realizar accion, en realidad es que no puede agarar porque no esta en contacto con otro objeto, por lo cual si tiene algun objeto el player lo puede soltar
        else if (thisT.childCount != 0)
        {
            i = Soltar(thisT, i);
        }

    }
}
