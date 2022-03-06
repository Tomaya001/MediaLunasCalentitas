using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericObject : MonoBehaviour
{
    public string id;
    public bool isPickable;
    public bool isTrash;

    private Animator animator;

    private void Awake()
    {
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
