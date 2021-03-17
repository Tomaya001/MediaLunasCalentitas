using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Transform t;
    public Joystick joystick;

    Animator animator;

    private void Start()
    {
        t = transform;
        animator = this.gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector3 algo = new Vector3(joystick.Horizontal, 0, joystick.Vertical) + t.position;
        t.LookAt(algo);
        if (joystick.Vertical != 0 || joystick.Horizontal !=0)
        {
            t.Translate(Vector3.forward * speed * Time.deltaTime);

            t.Rotate(new Vector3(0, -90, 0));
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        
    }
}
