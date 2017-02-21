using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterControl : MonoBehaviour
{
    private const float gravity = 9.80665f;

    public bool IsTurnedOn;
    private Vector3 cyclicVector;
    private Vector3 pedalsVector;
    private float cyclicX;
    private float cyclicZ;
    private float pedalsY;
    private float cyclicMultiplier = 50;
    private float pedalsMultiplier = 50;
    private float collectiveMultiplier = 50;
    private float maxCollective = 1;
    private float maxAltitude = 530;
    private float acceleration;
    private float maxAcceleration = 2;
    private float acelerationMultiplier2 = 0;

    float currentAltitude  ;
    public RawImage WindRose;

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().speed = 0;
        GetComponent<AudioSource>().pitch = 0;
        this.IsTurnedOn = false;
    }

    private void ApplyCollective(Vector2 cyclic, float pedals, float movementX, float movementY)
    {
        var movement = (movementX  != 0 || movementY != 0) ? true : false;
        int acelerationMultiplier = 0;
        acceleration = Mathf.Lerp(acceleration, movement ? maxAcceleration : 0, Time.deltaTime);
        //Debug.Log(acceleration);
       transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(movement ? -cyclic.y: cyclic.y, pedals, movement ? -cyclic.x: cyclic.x), Time.fixedDeltaTime * 5000);

        if (movementX != 0 && movementY != 0)
        {
            acelerationMultiplier = 36;
        }
        else
            acelerationMultiplier = 18;

        GetComponent<Rigidbody>().AddRelativeForce(cyclicX * acceleration, movement ? acelerationMultiplier * acceleration : gravity, -cyclicZ * acceleration, ForceMode.Acceleration);

    }

    void Update()
    {
        if (Input.GetButton(Constants.X))
        {
            IsTurnedOn = !IsTurnedOn;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.IsTurnedOn)
        {
            if (GetComponent<Animator>().speed < 1)
            {
                GetComponent<Animator>().speed += 0.008f;
                GetComponent<AudioSource>().pitch += 0.008f;
            }
            acelerationMultiplier2 = 0;
            moveCollective();

            Vector2 cyclic = moveCyclic();
            float pedals = movePedals();
            var movementX = Input.GetAxis(Constants.RightJoystickX);
            var movementY = Input.GetAxis(Constants.RightJoystickY);


             ApplyCollective(cyclic, pedals, movementX, movementY);
            //Debug.Log(cyclic.x * cyclicMultiplier + "\t" + pedals * pedalsMultiplier + "\t" + cyclic.y * cyclicMultiplier);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-cyclic.y, pedals, -cyclic.x), Time.fixedDeltaTime * 5000);
        }
        else
        {
            if (GetComponent<Animator>().speed > 0)
            {
                GetComponent<Animator>().speed -= 0.005f;
                GetComponent<AudioSource>().pitch -= 0.005f;
            }
            if (acelerationMultiplier2 < 100)
            acelerationMultiplier2 = acelerationMultiplier2 + 1.001f;
            GetComponent<Rigidbody>().AddForce(0,-1 * acelerationMultiplier2,0, ForceMode.Acceleration);
        }

        getVelocity();

        getAltitude();

    }

    public void getVelocity()
    {
        currentAltitude = transform.position.y;
        GameObject.Find("Canvas/Speedometer/Number").GetComponent<Text>().text = ((int)(GetComponent<Rigidbody>().velocity.magnitude*5)).ToString();
    }

    public void getAltitude()
    {
        GameObject.Find("Canvas/GameInfo/Altitude/Text/").GetComponent<Text>().text = String.Format("{0} m", currentAltitude.ToString("N2"));
    }

    public Vector2 moveCyclic()
    {
        cyclicVector.x = Input.GetAxis(Constants.RightJoystickX) * cyclicMultiplier;
        cyclicVector.z = -Input.GetAxis(Constants.RightJoystickY) * cyclicMultiplier;

        ///* ---------------------for keyboard---------------------- */
        //cyclicVector.x = (Input.GetAxis("Vertical")) * cyclicMultiplier;
        //cyclicVector.z = -(Input.GetAxis("Horizontal")) * cyclicMultiplier;
        ///* ---------------------for keyboard---------------------- */

        if (Input.GetAxis(Constants.RightJoystickY) != 0)
        {
            cyclicZ += cyclicVector.z * Time.fixedDeltaTime;
            cyclicZ = Mathf.Clamp(cyclicZ, -30f, 30f);
        }
        else
        {
            cyclicZ = Mathf.Lerp(cyclicZ, 0, Time.fixedDeltaTime);
        }

        if (Input.GetAxis(Constants.RightJoystickX) != 0)
        {
            cyclicX += cyclicVector.x * Time.fixedDeltaTime;
            cyclicX = Mathf.Clamp(cyclicX, -30f, 30f);
        }
        else
        {
            cyclicX = Mathf.Lerp(cyclicX, 0, Time.fixedDeltaTime);
        }

        //else
        //{
        //    cyclicX += -4 * Time.fixedDeltaTime;
        //    cyclicX = Mathf.Clamp(cyclicX, -30f, 30f);

        //    cyclicZ += -4 * Time.fixedDeltaTime;
        //    cyclicZ = Mathf.Clamp(cyclicZ, -30f, 30f);
        //}

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

        GameObject.Find("Canvas/WindRose/WindRoseImage").transform.rotation = Quaternion.Slerp(WindRose.transform.rotation, Quaternion.Euler(0, 0, pedalsY), Time.deltaTime);
         //WindRose.transform.rotation = Quaternion.Slerp(WindRose.transform.rotation, Quaternion.Euler(0, 0, pedalsY), Time.deltaTime);
        return pedalsY;
    }

    public void moveCollective()
    {
        /* DEFINIR LIMITE DE ROTAÇÃO MÁXIMA E MÍNIMA DAS HÉLICES PARA QUE O HELICÓPTERO FIQUE NO AR */
    
        float collective = Input.GetAxis(Constants.DPadY);

        if (currentAltitude <= maxAltitude)
        {
            if (collective != 0) {
                maxCollective = Mathf.Lerp(maxCollective, 1, Time.fixedDeltaTime);
            } else
            {
                maxCollective = Mathf.Lerp(maxCollective, 0, Time.fixedDeltaTime);
            }
            GetComponent<Rigidbody>().AddRelativeForce(0, collective * collectiveMultiplier * maxCollective, 0, ForceMode.Acceleration);

        }
        else
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, (Input.GetAxis(Constants.DPadY) -1) * collectiveMultiplier, 0, ForceMode.Acceleration);
        }


        // ---------------------for keyboard---------------------- //

        // ---------------------for keyboard---------------------- //

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain" )
        {
            if(GetComponent<HealthBar>().CurrentHealth > 0)
                GetComponent<HealthBar>().DealDamage((int)(collision.relativeVelocity.magnitude / 5));
        }
    }
}

