using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // ���� ī�޶� ã���ϴ�.
        mainCameraTransform =  GameObject.FindGameObjectWithTag("FallowCam").transform;
    }

    private void LateUpdate()
    {
        Vector3 directionToCamera = mainCameraTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
        transform.rotation = rotation;

      
    }
  
}
