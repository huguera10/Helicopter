using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterControl : MonoBehaviour
{
    private bool IsTurnedOn;
    private Vector3 cyclicVector;
    private Vector3 pedalsVector;
    private Vector3 collectiveVector;
    private float cyclicX;
    private float cyclicZ;
    private float pedalsY;
    //private CharacterController characterController;
    private float cyclicMultiplier = 50;
    private float pedalsMultiplier = 50;
    private float collectiveMultiplier = 30;
    //private float jumpPower = 200;
    //private float gravity = 10;

    private float upDown = 0.0f;
    private float UpDownVelocity;

    public RawImage WindRose;

    // Use this for initialization
    void Start()
    {
        //Physics.gravity = new Vector3(0, -5F, 0);
        //characterController = GetComponent<CharacterController>();
        this.IsTurnedOn = true;
    }

    float leftRightTurn;
    float Roll;
    float yValue;

    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (this.IsTurnedOn)
        {
            Vector2 cyclic = moveCyclic();

            float pedals = movePedals();

            moveCollective();

            Debug.Log(cyclic.x * cyclicMultiplier + "\t" + pedals * pedalsMultiplier + "\t" + cyclic.y * cyclicMultiplier);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cyclic.x, pedals, cyclic.y), Time.fixedDeltaTime * 5000);
        }
        //if (characterController.isGrounded)
        //{
        //    if (Input.GetButtonDown(Constants.A))
        //        this.IsTurnedOn = !this.IsTurnedOn;
        //    cyclicVector.y = 0;

        //    // ---------------------for keyboard---------------------- //
        //    if (Input.GetKey(KeyCode.Space))
        //    {
        //        cyclicVector.y = jumpPower;
        //    }
        //    // ---------------------for keyboard---------------------- //
        //}
    }

    private Vector2 moveCyclic()
    {

        cyclicVector.x = Input.GetAxis(Constants.RightJoystickX) * cyclicMultiplier;
        cyclicVector.z = Input.GetAxis(Constants.RightJoystickY) * cyclicMultiplier;

        /* ---------------------for keyboard---------------------- */
        cyclicVector.x = (Input.GetAxis("Vertical")) * cyclicMultiplier;
        cyclicVector.z = -(Input.GetAxis("Horizontal")) * cyclicMultiplier;
        /* ---------------------for keyboard---------------------- */

        //cyclicVector.x = Mathf.Clamp(cyclicVector.x, -1, 1);
        //cyclicVector.z = Mathf.Clamp(cyclicVector.z, -1, 1);

        cyclicX += cyclicVector.x * Time.fixedDeltaTime;
        cyclicX = Mathf.Clamp(cyclicX, -30f, 30f);

        cyclicZ += cyclicVector.z * Time.fixedDeltaTime;
        cyclicZ = Mathf.Clamp(cyclicZ, -30f, 30f);
        //cyclicVector.y -= gravity * Time.deltaTime;

        //characterController.Move(transform.TransformDirection(cyclicVector) * Time.deltaTime);
        //transform.Translate(cyclicVector * Time.deltaTime);
        return new Vector2(cyclicX, cyclicZ);
    }

    private float movePedals()
    {
        pedalsVector.y = (Input.GetAxis(Constants.RT) - Input.GetAxis(Constants.LT)) * pedalsMultiplier;

        /* ---------------------for keyboard---------------------- */
        if (Input.GetKey(KeyCode.J))
            pedalsVector.y = (-0.5f) * pedalsMultiplier;

        if (Input.GetKey(KeyCode.L))
            pedalsVector.y = (0.5f) * pedalsMultiplier;
        /* ---------------------for keyboard---------------------- */

        //pedalsVector.y = Mathf.Clamp(pedalsVector.y, -1, 1);

        pedalsY += pedalsVector.y * Time.deltaTime;

        WindRose.transform.rotation = Quaternion.Slerp(WindRose.transform.rotation, Quaternion.Euler(0, 0, pedalsY), Time.deltaTime);
        //transform.Rotate(pedalsVector * Time.deltaTime);
        return pedalsY;
    }

    private void moveCollective()
    {
        /* DEFINIR LIMITE DE ROTAÇÃO MÁXIMA E MÍNIMA DAS HÉLICES PARA QUE O HELICÓPTERO FIQUE NO AR */
        //collectiveVector.y = Input.GetAxis(Constants.DPadY) * collectiveSpeed;
        if (Input.GetAxis(Constants.DPadY) > 0)
        {
            Physics.gravity += Vector3.up * 2;
        }
        else
        {
            if (Input.GetAxis(Constants.DPadY) < 0)
            {
                Physics.gravity += Vector3.down * 2;
            }
            else
            {
                Physics.gravity += -Physics.gravity * Time.deltaTime / 2;
            }
        }
        //Debug.Log(String.Format("DPadY value: {0}", Input.GetAxis(Constants.DPadY)));

        // ---------------------for keyboard---------------------- //
        //if (Input.GetKey(KeyCode.I))
        //    collectiveVector.y = (5f) * collectiveSpeed;

        //if (Input.GetKey(KeyCode.K))
        //    collectiveVector.y = (-5f) * collectiveSpeed;

        //if (Input.GetKey(KeyCode.I))
        //    Physics.gravity += Vector3.up;
        //else
        //{
        //    if (Input.GetKey(KeyCode.K))
        //    {
        //        Physics.gravity += Vector3.down;
        //    }
        //    else
        //    {
        //        Physics.gravity += -Physics.gravity * Time.deltaTime;
        //    }
        //}
        // ---------------------for keyboard---------------------- //

        //collectiveVector.y -= gravity * 2;// * Time.deltaTime 10;

        //characterController.Move(transform.TransformDirection(collectiveVector) * Time.deltaTime);
    }

}

