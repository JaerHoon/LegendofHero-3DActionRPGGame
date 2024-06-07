using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Npc¡¢√À");
        }
    }
}
