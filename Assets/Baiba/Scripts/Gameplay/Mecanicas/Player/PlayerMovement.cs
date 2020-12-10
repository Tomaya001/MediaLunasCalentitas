using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Transform t;
    public Joystick joystick;

    private void Start()
    {
        t = transform;
    }

    private void Update()
    {
        Vector3 algo = new Vector3(joystick.Horizontal, 0, joystick.Vertical) + t.position;
        t.LookAt(algo);
        if (joystick.Vertical != 0 || joystick.Horizontal !=0)
        {
            t.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
