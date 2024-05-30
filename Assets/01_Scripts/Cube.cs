using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
<<<<<<< HEAD
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Ãæµ¹Cube");
=======
    
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        
>>>>>>> GuHakGyun
    }
}
