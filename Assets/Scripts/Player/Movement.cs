using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public VariableJoystick joystick;
    public Animator animCtrl;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    public float Speed = 15f;
    public float RotationSpeed = 10f;

    public float movementVector;

    //public Vector3 movementCache = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (joystick == null)
            return;

        if (animCtrl == null)
            return;


        Vector2 direction = joystick.Direction;
        float movementVector = direction.magnitude;
        movementVector = movementVector * Time.deltaTime * Speed;
        transform.position += new Vector3(direction.x, 0, direction.y) * movementVector;
        //movementCache += movementVector;

        if (movementVector > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up), Time.deltaTime * RotationSpeed);
        }


        //bool isWalking = direction != Vector2.zero;
        bool isWalking = direction.magnitude > 0;

        animCtrl.SetBool("IsWalking", isWalking);

        animCtrl.SetFloat("SpeedValue", direction.magnitude);

    }

    //private void FixedUpdate()
    //{
    //    if (movementCache != Vector3.zero)
    //    {
    //        transform.position += movementCache;
    //        movementCache = Vector3.zero;  
    //    }

    //}



}
