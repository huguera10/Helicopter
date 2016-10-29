using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBoxControl : MonoBehaviour
{

    private Vector3 cyclicVector;
    private Vector3 pedalsVector;
    private Vector3 collectiveVector;
    private CharacterController characterController;
    private float cyclicSpeed = 100;
    private float pedalsSpeed = 50;
    private float collectiveSpeed = 50;
    private float jumpPower = 150;
    private float gravity = 150;


    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCyclic();

        movePedals();

        moveCollective();

        if (characterController.isGrounded)
        {
            Debug.Log("Bateu, seu juvena");
            cyclicVector.y = 0;

            if (Input.GetButtonDown(Constants.A))
            {
                cyclicVector.y = jumpPower;
            }
        }
    }

    private void moveCyclic() {
        cyclicVector.x = Input.GetAxis(Constants.LeftJoystickX) * cyclicSpeed;
        cyclicVector.z = Input.GetAxis(Constants.LeftJoystickX) * cyclicSpeed;

        cyclicVector.y -= gravity * Time.deltaTime;

        characterController.Move(transform.TransformDirection(cyclicVector) * Time.deltaTime);
    }

    private void movePedals()
    {
        pedalsVector.y = (Input.GetAxis(Constants.RT) - Input.GetAxis(Constants.LT)) * pedalsSpeed;

        transform.Rotate(pedalsVector * Time.deltaTime);
    }

    private void moveCollective()
    {
        collectiveVector.y = Input.GetAxis(Constants.DPadY) * collectiveSpeed;

        characterController.Move(transform.TransformDirection(collectiveVector) * Time.deltaTime);
    }
}
