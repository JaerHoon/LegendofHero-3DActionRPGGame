using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItBox : MonoBehaviour
{
    Monster monster;

    private void Start()
    {
        monster = transform.GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterDamage player = other.GetComponent<CharacterDamage>();
            
            monster.monsterAtk.Hit(player);
        }

        
    }
}
