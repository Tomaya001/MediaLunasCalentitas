using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : PlayerActions
{    
    public Transform[] points;
    protected int i;

    private Transform thisT;

    private void Start()
    {
        i = 0;
        thisT = transform;
    }

    protected int Pick(Transform[] _points, Transform p, int _i)
    {
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
    
    /*protected void Soltar()
    {
        for (int j = 0; j < thisT.childCount;  j++)
        {
            thisT.GetChild(j).gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            thisT.GetChild(j).SetParent(null);
            busyHand = false;
            i--;
        }
    }*/

    public void ActionPlayer()
    {
        if (canActionPlayer)
        {
            if(thisT.GetComponentInChildren<Bandeja>())
            {
                if(thisT.GetComponentInChildren<Bandeja>().points.Length > thisT.GetComponentInChildren<Bandeja>().i)
                {
                    thisT.GetComponentInChildren<Bandeja>().i = Pick(thisT.GetComponentInChildren<Bandeja>().points, thisT.GetComponentInChildren<Bandeja>().transform, 
                        thisT.GetComponentInChildren<Bandeja>().i);
                }
            }
            i = Pick(points, thisT, i);
        }
        else if (thisT.childCount != 0)
        {
            i = Soltar(thisT, i);
            /*if (this.GetComponentInChildren<Bandeja>())
            {
                if (this.GetComponentInChildren<Bandeja>().canbeReleased)
                {
                    Soltar(thisT);

                    
                    for (int i = 0; i < thisT.childCount; i++)
                    {
                        if (thisT.GetChild(i).GetComponent<Bandeja>())
                        {
                            thisT.GetChild(i).GetComponent<Rigidbody>().detectCollisions = true;
                            thisT.GetChild(i).SetParent(null);
                        }
                    }
                }
            }
            else
            {
                Soltar(thisT);
            }*/
        }

    }
}
