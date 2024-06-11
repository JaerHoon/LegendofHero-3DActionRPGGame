using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_ItemRelic : Platform
{
    void Start()
    {
        Init();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnSkillInfo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OffSkillInfo();
        }
    }

}
