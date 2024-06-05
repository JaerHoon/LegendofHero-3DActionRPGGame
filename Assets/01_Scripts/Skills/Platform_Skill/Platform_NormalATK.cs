using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_NormalATK : MonoBehaviour
{
    SphereCollider coll;

 

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("¡¯¿‘");
        }
    }
}
