using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 centre;
    public float baseFOV;
    [Range(0f, 1f)]
    public float lookStrength;
    [Range(0f, 1f)]
    public float zoomStrength;

    private void LateUpdate()
    {
        RotateTowards(target);
        ZoomOn(target);
    }

    void RotateTowards(Transform target)
    {
        transform.LookAt(target.position / (1 / lookStrength) - centre);
    }

    void ZoomOn(Transform target)
    {
        GetComponent<Camera>().fieldOfView = baseFOV - (target.position.z / (1 / zoomStrength));
    }
}
