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
    //private CharacterController characterController;
    private float cyclicSpeed = 50;
    private float pedalsSpeed = 50;
    private float collectiveSpeed = 30;
    private float jumpPower = 200;
    //private float gravity = 10;

    private float upDown = 0.0f;

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
        cyclicVector.x = (Input.GetAxis("Horizontal")) * cyclicSpeed;

        if (cyclicVector.x > 0)
        {
            leftRightTurn  += Time.deltaTime * 1.5f;
        }
        else {
            if (cyclicVector.x < 0)
            {
                leftRightTurn -= Time.deltaTime * 1.5f;
            } else
            {
                leftRightTurn = Mathf.SmoothDamp(leftRightTurn, 0, ref yValue, 0.1f);
            }
        }

        leftRightTurn = Mathf.Clamp(leftRightTurn, -1, 1);

        Roll += leftRightTurn * Time.fixedDeltaTime;
        Roll = Mathf.Clamp(Roll, -1.2f, 1.2f);

        Debug.Log(Roll);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Roll*10), Time.fixedDeltaTime * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.IsTurnedOn)
        {
            moveCyclic();

            movePedals();

            moveCollective();
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

    private void moveCyclic()
    {


        cyclicVector.x = Input.GetAxis(Constants.RightJoystickX) * cyclicSpeed;
        cyclicVector.z = Input.GetAxis(Constants.RightJoystickY) * cyclicSpeed;

        /* ---------------------for keyboard---------------------- */
        //cyclicVector.z = -(Input.GetAxis("Vertical")) * cyclicSpeed;
        //cyclicVector.x = -(Input.GetAxis("Horizontal")) * cyclicSpeed;
        /* ---------------------for keyboard---------------------- */

        //cyclicVector.y -= gravity * Time.deltaTime;

        //characterController.Move(transform.TransformDirection(cyclicVector) * Time.deltaTime);
        transform.Translate(cyclicVector * Time.deltaTime);
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

        if (Input.GetKey(KeyCode.I))
            Physics.gravity += Vector3.up * 2;
        else
        {
            if (Input.GetKey(KeyCode.K))
            {
                Physics.gravity += Vector3.down * 2;
            }
            else
            {
                Physics.gravity += -Physics.gravity * Time.deltaTime /2;
            }
        }
        // ---------------------for keyboard---------------------- //

        //collectiveVector.y -= gravity * 2;// * Time.deltaTime 10;

        //characterController.Move(transform.TransformDirection(collectiveVector) * Time.deltaTime);
    }
}

