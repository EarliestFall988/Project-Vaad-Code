using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterController : MonoBehaviour
{
    public float Speed = 1f;
    public float jumpPower;
    public float turnSpeed = 5;

    CharacterController cc;

    float y = 0;

    public Transform cameraTransform;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCharacter();
        RotateCharacter();
    }

    void MoveCharacter()
    {
        float x = Input.GetAxis("Horizontal") * Speed;
        float z = Input.GetAxis("Vertical") * Speed;

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            y = jumpPower;
        }

        if (!cc.isGrounded)
        {
            y -= 9.8f * Time.deltaTime;
        }
        else
        {
            y -= Mathf.Epsilon;
        }

        Vector3 movement = new Vector3(x, y, z);
        movement = transform.rotation * movement;


        cc.Move(movement * Time.deltaTime);
    }

    void RotateCharacter()
    {
        float rotX = Input.GetAxis("Mouse X") * turnSpeed;
        float rotY = Input.GetAxis("Mouse Y") * turnSpeed;

        transform.Rotate(0, rotX, 0);

        cameraTransform.Rotate(-rotY, 0, 0);
    }
}
