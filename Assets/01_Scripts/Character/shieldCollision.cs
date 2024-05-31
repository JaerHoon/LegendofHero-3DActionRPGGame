using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollision : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("방패 충돌!!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
