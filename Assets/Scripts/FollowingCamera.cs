using System;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform target;
    public float minDistance;
    public float minHeight;
    private float joystickX;
    private float joystickY;

    private float maxRotation = 2.5f;
    private float minRotation = 1f;
    private float rotation = 1;

    public Camera camera2;

    // Use this for initialization
    void Start()
    {
        camera2.enabled = false;
        //camera2.GetComponent<AudioSource>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Y))
        {
            GetComponent<Camera>().enabled = !GetComponent<Camera>().enabled;
            //GetComponent<AudioSource>().enabled = !GetComponent<AudioSource>().enabled;

            camera2.enabled = !camera2.enabled;
            //camera2.GetComponent<AudioSource>().enabled = !camera2.GetComponent<AudioSource>().enabled;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        joystickX = Input.GetAxis(Constants.LeftJoystickX);
        joystickY = Input.GetAxis(Constants.LeftJoystickY);

        Vector3 wantedPosition = target.TransformPoint(0, minHeight, minDistance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * 2);

        if (GetComponent<Camera>().enabled == true)
        {
            if (joystickX != 0 || joystickY != 0)
            {
                rotation = Mathf.Lerp(rotation, maxRotation, Time.deltaTime);
                transform.RotateAround(target.transform.position, new Vector3(-joystickY, joystickX, 0), rotation);
            }

            rotation = Mathf.Lerp(rotation, minRotation, Time.deltaTime);

            transform.LookAt(target, target.up);
        }
        else
        {
            Debug.Log(-joystickY);
            if (joystickX != 0 || joystickY != 0)
            {
                camera2.transform.localRotation = Quaternion.Slerp(camera2.transform.localRotation, Quaternion.Euler(10 - joystickY * 30, -180 - joystickX * 50, 0), Time.fixedDeltaTime * 4);
            }
            else
            {
                camera2.transform.localRotation = Quaternion.Slerp(camera2.transform.localRotation, Quaternion.Euler(10, -180, 0), Time.fixedDeltaTime * 4);
            }
        }
    }
}
