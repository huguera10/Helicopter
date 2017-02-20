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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        joystickX = Input.GetAxis(Constants.LeftJoystickX);
        joystickY = Input.GetAxis(Constants.LeftJoystickY);

        Vector3 wantedPosition = target.TransformPoint(0, minHeight, minDistance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * 2);

        if (joystickX != 0 || joystickY != 0)
        {
            rotation = Mathf.Lerp(rotation, maxRotation, Time.deltaTime);
            transform.RotateAround(target.transform.position, new Vector3(-joystickY, joystickX, 0), rotation);
        }

        rotation = Mathf.Lerp(rotation, minRotation, Time.deltaTime);
        //if (joystickY != 0)
        //{
        //    transform.RotateAround(target.transform.position, new Vector3(joystickY, 0, 0), 1);
        //}

        transform.LookAt(target, target.up);
    }
}
