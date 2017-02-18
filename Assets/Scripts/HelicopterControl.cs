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
    private float cyclicMultiplier = 50;
    private float pedalsMultiplier = 50;
    private float collectiveMultiplier = 30;
    //float power = 0, acceleration = 1;
    int aux = 0;

    public RawImage WindRose;

    // Use this for initialization
    void Start()
    {
        this.IsTurnedOn = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (this.IsTurnedOn)
        {

            //Debug.Log(Input.GetAxis(Constants.RightJoystickY));
            //if (Input.GetAxis(Constants.RightJoystickY) > 0)
            //{
            //    aux = 1;
            //    power = power - (acceleration);
            //    GetComponent<Rigidbody>().AddForce((transform.forward * 5 * power * (Input.GetAxis(Constants.RightJoystickY) * -1)), ForceMode.Acceleration);


            //}
            //else if (Input.GetAxis(Constants.RightJoystickY) < 0)
            //{
            //    aux = -1;
            //    power = power + (acceleration);
            //    GetComponent<Rigidbody>().AddForce((transform.forward * 5 * power * (Input.GetAxis(Constants.RightJoystickY) * 1)), ForceMode.Acceleration);
            //}else
            //{
            //    if (power != 0)
            //    {
            //        power = power + (acceleration) * aux; 
            //    }
            //    GetComponent<Rigidbody>().AddForce((transform.forward * 5 * power), ForceMode.Acceleration);
            //}



            Vector2 cyclic = moveCyclic();
            float pedals = movePedals();
            //Debug.Log(cyclic.x * cyclicMultiplier + "\t" + pedals * pedalsMultiplier + "\t" + cyclic.y * cyclicMultiplier);
            //GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(cyclic.y, pedals, cyclic.x), Time.fixedDeltaTime * 5000));

            GetComponent<Rigidbody>().AddRelativeForce(0,20,-cyclicZ * 5, ForceMode.Acceleration);
            GetComponent<Rigidbody>().AddRelativeForce(-cyclicX * 5 , 20, 0, ForceMode.Acceleration);

            moveCyclic();

            movePedals();

            moveCollective();

            //Debug.Log(cyclic.x * cyclicMultiplier + "\t" + pedals * pedalsMultiplier + "\t" + cyclic.y * cyclicMultiplier);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-cyclic.y, pedals, cyclic.x), Time.fixedDeltaTime * 5000);
        }

    }

    public Vector2 moveCyclic()
    {
        cyclicVector.x = Input.GetAxis(Constants.RightJoystickX) * cyclicMultiplier;
        cyclicVector.z = -Input.GetAxis(Constants.RightJoystickY) * cyclicMultiplier;

        ///* ---------------------for keyboard---------------------- */
        //cyclicVector.x = (Input.GetAxis("Vertical")) * cyclicMultiplier;
        //cyclicVector.z = -(Input.GetAxis("Horizontal")) * cyclicMultiplier;
        ///* ---------------------for keyboard---------------------- */


        cyclicX += cyclicVector.x * Time.fixedDeltaTime;
        cyclicX = Mathf.Clamp(cyclicX, -30f, 30f);

        cyclicZ += cyclicVector.z * Time.fixedDeltaTime;
        cyclicZ = Mathf.Clamp(cyclicZ, -30f, 30f);

        return new Vector2(cyclicX, cyclicZ);
    }

    public float movePedals()
    {
        pedalsVector.y = (Input.GetAxis(Constants.RT) - Input.GetAxis(Constants.LT)) * pedalsMultiplier;

        /* ---------------------for keyboard---------------------- */
        if (Input.GetKey(KeyCode.J))
            pedalsVector.y = (-0.5f) * pedalsMultiplier;

        if (Input.GetKey(KeyCode.L))
            pedalsVector.y = (0.5f) * pedalsMultiplier;
        /* ---------------------for keyboard---------------------- */


        pedalsY += pedalsVector.y * Time.deltaTime;

        WindRose.transform.rotation = Quaternion.Slerp(WindRose.transform.rotation, Quaternion.Euler(0, 0, pedalsY), Time.deltaTime);
        return pedalsY;
    }

    public void moveCollective()
    {
        /* DEFINIR LIMITE DE ROTAÇÃO MÁXIMA E MÍNIMA DAS HÉLICES PARA QUE O HELICÓPTERO FIQUE NO AR */
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

    }

}

