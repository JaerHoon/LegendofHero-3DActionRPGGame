using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTrigger : MonoBehaviour
{
    BoxCollider box;
    MonsterBuff Freeze;
    void Start()
    {
        box = GetComponent<BoxCollider>();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(1);
            //Freeze.GetComponent<MonsterBuff>().Onfreeze(10.0f, 1.0f, 1.0f);
            /*if(ArcherAttack.instance.isFreeze==true)
            {
                Freeze.GetComponent<Freeze>().OnBuff(10.0f, 1f, 1f);
            }*/
        }        
    }

    public void archerOnCollider()
    {
        Invoke("archeroffCollider", 0.2f);
        Invoke("archerOnColliders" ,0.1f);

        if(ArcherAttack.instance != null && ArcherAttack.instance.isButtonPressed2)
        {
            Invoke("archeroffCollider", 0.4f * Time.deltaTime);
            Invoke("archerOnColliders", 0.2f * Time.deltaTime);
        }

       
        
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
