using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {

            other.GetComponent<MonsterDamage>().OnDamage(5);

            //Debug.Log("충돌감지!!");

        }

    }

    public void OnCollider()
    {
        Invoke("OnColliders", 0.2f);
        Invoke("offCollider", 0.3f);
    }

    void OnColliders()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    void offCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
