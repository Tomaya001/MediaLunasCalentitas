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
                    _points[_i].gameObject.GetComponent<PuntoRefScript>().ocupado = true;
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
            points[0].gameObject.GetComponent<PuntoRefScript>().ocupado = false;
        }
        animator.SetBool("Pick", false);
        return _i;
    }
    
    public void Limpiar(Transform point)
    {
        for (int j = 0; j < point.childCount; j++)
        {
            if (point.GetChild(j).GetComponent<GenericObject>())
            {                
                point.GetChild(j).gameObject.SetActive(false);
                point.GetChild(j).SetParent(null);
                j--;
            }
        }
        if (point.gameObject.GetComponent<Bandeja>())
        {
            point.gameObject.GetComponent<Bandeja>().i = 0;
            point.gameObject.GetComponent<Bandeja>().Descopupar(point);
            point.position = point.gameObject.GetComponent<Bandeja>().possInicial;
            point.rotation = point.gameObject.GetComponent<Bandeja>().rootInicial;
            point.SetParent(null);
            animator.SetBool("Pick", false);
        }
        else
        {
            point.gameObject.GetComponent<PickUp>().i = 0;
            point.gameObject.GetComponent<PickUp>().Descopupar(point);
            animator.SetBool("Pick", false);
        }
            
            

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

    private void Descopupar(Transform p)
    {
        for (int j = 0; j < p.childCount; j++)
        {
            if (p.GetChild(j).gameObject.GetComponent<PuntoRefScript>())
            {
                p.GetChild(j).gameObject.GetComponent<PuntoRefScript>().ocupado = false;
            }
        }
    }


    public void ActionPlayer()
    {
        //Preguntamos si el jugador puede realizar una accion que se activa cuando el jugador esta colisionando con algun trigger
        if (canActionPlayer)
        {
            if (t.gameObject.GetComponent<SacarPorcionScript>())
            {
                if (thisT.gameObject.GetComponentInChildren<Bandeja>())
                {
                    if(thisT.GetChild(0).gameObject.GetComponent<Bandeja>().points.Length > thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i)
                    {
                        t.gameObject.GetComponent<SacarPorcionScript>().SacarPorcion(
                            thisT.GetChild(0).GetComponent<Bandeja>().points[
                                thisT.GetChild(0).GetComponent<Bandeja>().i],true);
                        thisT.GetChild(0).GetComponent<Bandeja>().points[thisT.GetChild(0).GetComponent<Bandeja>().i].gameObject.GetComponent<PuntoRefScript>().ocupado = true;
                        thisT.GetChild(0).GetComponent<Bandeja>().i++;
                    }                        
                    else
                    {
                        Debug.Log("Bandeja Llena");
                    }
                }                              
                else
                {
                    animator.SetBool("Pick", true);
                    t.gameObject.GetComponent<SacarPorcionScript>().SacarPorcion(thisT,false);
                }
                    
            }

            else if (t.gameObject.GetComponent<CafeteraScripts>())
            {
                int cont = 0;
                if (t.GetChild(0).childCount == 0) 
                {
                    if (thisT.childCount != 0)
                    {
                        if (!thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                        {
                            switch (thisT.GetChild(0).GetComponent<GenericObject>().id)
                            {
                                case "Taza":
                                    t.gameObject.GetComponent<CafeteraScripts>().AbrirInventario(thisT, thisT.GetChild(0));
                                    animator.SetBool("Pick", false);
                                    points[0].gameObject.GetComponent<PuntoRefScript>().ocupado = false;
                                    thisT.gameObject.GetComponent<PickUp>().i--;
                                    break;
                            }
                        }
                        else
                        {
                            for (int a = 0; a < thisT.GetChild(0).childCount; a++)
                            {
                                if (thisT.GetChild(0).GetChild(a).gameObject.GetComponent<GenericObject>())
                                {
                                    if (thisT.GetChild(0).GetChild(a).gameObject.GetComponent<GenericObject>().id == "Taza")
                                    {
                                        t.gameObject.GetComponent<CafeteraScripts>().AbrirInventario(thisT.GetChild(0), thisT.GetChild(0).GetChild(a));
                                        thisT.GetChild(0).gameObject.GetComponent<Bandeja>().points[cont].gameObject.GetComponent<PuntoRefScript>().ocupado = false;
                                        animator.SetBool("Pick", false);
                                        cont++;
                                    }                                        
                                }
                            }
                        }
                    }
                }
                else
                {
                    if(thisT.childCount == 0)
                    {
                        bool aux = t.gameObject.GetComponent<CafeteraScripts>().SacarTaza(thisT);
                        if (aux)
                            animator.SetBool("Pick", true);
                        
                    }                    
                    else
                    {
                        if (thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                        {
                            for (int a = 0; a < thisT.GetChild(0).childCount; a++)
                            {
                                if(thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>())
                                {
                                    if (!thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>().ocupado)
                                    {
                                        t.gameObject.GetComponent<CafeteraScripts>().SacarTaza(thisT.GetChild(0), thisT.GetChild(0).GetChild(a));
                                        thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>().ocupado = true;
                                        break;
                                    }
                                }
                                cont++;
                            }
                        }                            
                    }

                      
                }   
                
            }

            else if (t.gameObject.GetComponent<EntregaScript>())
            {
                if(!thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                {
                    t.gameObject.GetComponent<EntregaScript>().ComprobarOrden(thisT);
                }
                else
                {
                    thisT.GetChild(0).GetComponent<Bandeja>().i = 0;
                    thisT.gameObject.GetComponent<PickUp>().i--;
                    t.gameObject.GetComponent<EntregaScript>().ComprobarOrden(thisT.GetChild(0));                                        
                }
            }

            else if (t.gameObject.GetComponent<HeladeraScript>())
            {
                int cont = 0;
                if (thisT.childCount == 0)
                {
                    t.gameObject.GetComponent<HeladeraScript>().AbrirInventario(thisT);
                    animator.SetBool("Pick", true);
                }
                else
                {
                    if (thisT.GetChild(0).GetComponent<Bandeja>())
                    {
                        for (int a = 0; a < thisT.GetChild(0).childCount; a++)
                        {
                            if (thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>())
                            {
                                if (!thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>().ocupado)
                                {
                                    t.gameObject.GetComponent<HeladeraScript>().AbrirInventario(thisT.GetChild(0).GetChild(a));
                                    thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i = thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i++;
                                    thisT.GetChild(0).GetChild(a).gameObject.GetComponent<PuntoRefScript>().ocupado = true;
                                    break;
                                }
                            }
                            cont++;
                        }
                    }
                }
            }

            /*De no tener la bandeja le pasamos los datos de la mano del player*/
            else if (t.gameObject.GetComponent<GenericObject>())
            {
                if(t.gameObject.GetComponent<GenericObject>().id == "Tacho Basura")
                {
                    if (thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                    {
                        Limpiar(thisT.GetChild(0));
                    }
                    else
                    {
                        Limpiar(thisT);
                    }
                }

                if(thisT.childCount != 0)
                {
                    if (thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                    {
                        thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i = Pick(
                            thisT.GetChild(0).gameObject.GetComponent<Bandeja>().points,
                            thisT.GetChild(0),
                            thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i);
                    }
                }
                else
                {
                    i = Pick(points, thisT, i);
                }
                
            }

        }

        //De no poder realizar accion, en realidad es que no puede agarar porque no esta en contacto con otro objeto, por lo cual si tiene algun objeto el player lo puede soltar
        else if (thisT.childCount != 0)
        {
            i = Soltar(thisT, i);
        }        
    }
}
