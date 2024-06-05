using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStagePortal : MonoBehaviour
{
    SphereCollider coll;

    void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("포탈범위진입");
        }
    }
}
