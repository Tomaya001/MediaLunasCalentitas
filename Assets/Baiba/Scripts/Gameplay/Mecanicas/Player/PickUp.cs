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
            if (t.gameObject.GetComponent<SacarPorcionScript>())
            {
                if (thisT.gameObject.GetComponentInChildren<Bandeja>())
                {
                    if(thisT.GetChild(0).gameObject.GetComponent<Bandeja>().points.Length > thisT.GetChild(0).gameObject.GetComponent<Bandeja>().i)
                    {
                        t.gameObject.GetComponent<SacarPorcionScript>().SacarPorcion(
                            thisT.GetChild(0).GetComponent<Bandeja>().points[
                                thisT.GetChild(0).GetComponent<Bandeja>().i],true);
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
                if(thisT.childCount != 0)
                {
                    if (!thisT.GetChild(0).gameObject.GetComponent<Bandeja>())
                    {
                        switch (thisT.GetChild(0).GetComponent<GenericObject>().id)
                        {
                            case "Taza":
                                t.gameObject.GetComponent<CafeteraScripts>().ActivarCafetera(thisT.GetChild(0));
                                animator.SetBool("Pick", false);
                                break;
                        }
                    }
                    else
                    {
                        for (int a = 0; a < thisT.GetChild(0).childCount ; a++)
                        {
                            if (thisT.GetChild(0).GetChild(a).gameObject.GetComponent<GenericObject>())
                            {
                                switch (thisT.GetChild(0).GetChild(a).gameObject.GetComponent<GenericObject>().id)
                                {
                                    case "Taza":
                                        t.gameObject.GetComponent<CafeteraScripts>().ActivarCafetera(thisT.GetChild(0).GetChild(a));
                                        animator.SetBool("Pick", false);
                                        break;
                                }
                            }


                        }
                    }
                    
                }
                else
                {
                    bool aux = t.gameObject.GetComponent<CafeteraScripts>().SacarTaza(thisT);
                    if (aux)
                        animator.SetBool("Pick", true);
                }
            }
            /*De no tener la bandeja le pasamos los datos de la mano del player*/
            else if (t.gameObject.GetComponent<GenericObject>())
            {
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
