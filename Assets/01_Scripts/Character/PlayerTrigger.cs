using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    //[SerializeField]
    //ScriptableObject playerDamage;
    Warrior controller;
    
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        controller = GameObject.FindWithTag("Player").GetComponent<Warrior>();
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(controller.playerSkillsSlot[0].damage);

            //Debug.Log("충돌감지!!");
        }
        else if (other.gameObject.tag == "Dummy" )
        {
            other.GetComponent<Dummy>().OnHit(controller.playerSkillsSlot[0].damage);
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
