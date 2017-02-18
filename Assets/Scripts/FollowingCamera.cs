using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform target;
    public float minDistance;
    public float minHeight;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 wantedPosition = target.TransformPoint(0, minHeight, minDistance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * 2);

        transform.LookAt(target, target.up);
    }
}
