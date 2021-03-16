using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : PlayerActions
{    
    public Transform[] points;
    protected int i;

    private Transform thisT;

    private void Awake()
    {
        i = 0;
        thisT = this.transform;
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
                    t.position = _points[_i].position;
                    t.SetParent(p);
                    t.GetComponent<Rigidbody>().detectCollisions = false;
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
            p.GetChild(j-1).gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            p.GetChild(j-1).SetParent(null);
            _i--;
        }
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
            /*Preguntamos si dentro de su herencia de objeto se encuentra la bandeja, de ser asi al Procedimiento Pick le mandamos los puntos y el tranform de la bandeja para setearla como padre
            de los objetos*/
            if (thisT.GetComponentInChildren<Bandeja>())
            {
                if(thisT.GetComponentInChildren<Bandeja>().points.Length > thisT.GetComponentInChildren<Bandeja>().i)
                {
                    thisT.GetComponentInChildren<Bandeja>().i = Pick(thisT.GetComponentInChildren<Bandeja>().points, thisT.GetComponentInChildren<Bandeja>().transform, 
                        thisT.GetComponentInChildren<Bandeja>().i);
                }
            }
            /*De no tener la bandeja le pasamos los datos de la mano del player*/
            else
                i = Pick(points, thisT, i);
        }
        //De no poder realizar accion, en realidad es que no puede agarar porque no esta en contacto con otro objeto, por lo cual si tiene algun objeto el player lo puede soltar
        else if (thisT.childCount != 0)
        {
            i = Soltar(thisT, i);
        }

    }
}
