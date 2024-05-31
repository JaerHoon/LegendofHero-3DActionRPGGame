using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTrigger : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Monster")
        {
            Debug.Log("충돌되었습니다!");
        }
    }

    public void archerOnCollider()
    {
        Invoke("archeroffCollider", 0.5f*Time.deltaTime);
        Invoke("archerOnColliders", 0.3f*Time.deltaTime);
    }

   

    void archerOnColliders()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    void archeroffCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        archerOnCollider();
    }
}
