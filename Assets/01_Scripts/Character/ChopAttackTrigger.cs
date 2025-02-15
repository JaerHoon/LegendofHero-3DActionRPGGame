using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopAttackTrigger : MonoBehaviour
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

            //Debug.Log("�浹����!!");

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
