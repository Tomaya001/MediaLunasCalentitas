using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericObject : MonoBehaviour
{
    public string id;
    public bool isPickable;
    public bool isTrash;
    public bool activo;

    private Animator animator;
    
    private float tiempo;

    private void Awake()
    {
        tiempo = 3f;
        if (gameObject.GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
        }
        else
        {
            animator = null;
        }
        if(gameObject.GetComponent<Outline>())
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<Renderer>())
        {
            if (!activo)
            {
                if (tiempo > 0f)
                {
                    tiempo -= Time.deltaTime;
                }
                else
                {
                    this.gameObject.GetComponent<Renderer>().enabled = true;
                    activo = true;
                    tiempo = 3f;
                }
            }
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(id == "Taza")
        {
            if (collision.gameObject.CompareTag("Piso"))
            {
                isTrash = true;
                animator.SetBool("Trash", true);
            }
        }
    }
}
