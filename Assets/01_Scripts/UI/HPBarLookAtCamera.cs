using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarLookAtCamera : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("FallowCam").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,
                         cam.rotation * Vector3.up);
    }

}
