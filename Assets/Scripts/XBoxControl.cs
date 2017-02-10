using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XBoxControl : MonoBehaviour
{

    private Vector3 cyclicVector;
    private Vector3 pedalsVector;
    private Vector3 collectiveVector;
    private CharacterController characterController;
    private float cyclicSpeed = 100;
    private float pedalsSpeed = 50;
    private float collectiveSpeed =30;
    private float jumpPower = 200;
    private float gravity = 10;

    public RawImage WindRose;

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
            cyclicVector.y = 0;

            if (Input.GetButtonDown(Constants.A))
            {
                cyclicVector.y = jumpPower;
            }

            // ---------------------for keyboard---------------------- //
            if (Input.GetKey(KeyCode.Space))
            {
                cyclicVector.y = jumpPower;
            }
            // ---------------------for keyboard---------------------- //
        }
    }

    private void moveCyclic()
    {
        cyclicVector.x = Input.GetAxis(Constants.LeftJoystickX) * cyclicSpeed;
        cyclicVector.z = Input.GetAxis(Constants.LeftJoystickX) * cyclicSpeed;

        /* ---------------------for keyboard---------------------- */
        cyclicVector.z = (Input.GetAxis("Vertical")) * cyclicSpeed;
        cyclicVector.x = (Input.GetAxis("Horizontal")) * cyclicSpeed;
        /* ---------------------for keyboard---------------------- */

        //cyclicVector.y -= gravity * Time.deltaTime;

        characterController.Move(transform.TransformDirection(cyclicVector) * Time.deltaTime);
    }

    private void movePedals()
    {
        pedalsVector.y = (Input.GetAxis(Constants.RT) - Input.GetAxis(Constants.LT)) * pedalsSpeed;

        /* ---------------------for keyboard---------------------- */
        if (Input.GetKey(KeyCode.J))
            pedalsVector.y = (-0.5f) * pedalsSpeed;

        if (Input.GetKey(KeyCode.L))
            pedalsVector.y = (0.5f) * pedalsSpeed;
        /* ---------------------for keyboard---------------------- */

        WindRose.transform.Rotate(0, 0, pedalsVector.y * Time.deltaTime);
        transform.Rotate(pedalsVector * Time.deltaTime);
    }

    private void moveCollective()
    {
        collectiveVector.y = Input.GetAxis(Constants.DPadY) * collectiveSpeed;

        // ---------------------for keyboard---------------------- //
        if (Input.GetKey(KeyCode.I))
            collectiveVector.y = (5f) * collectiveSpeed;

        if (Input.GetKey(KeyCode.K))
            collectiveVector.y = (-5f) * collectiveSpeed;
        // ---------------------for keyboard---------------------- //

        collectiveVector.y -= gravity * 2;// * Time.deltaTime 10;

        characterController.Move(transform.TransformDirection(collectiveVector) * Time.deltaTime);
    }
}

