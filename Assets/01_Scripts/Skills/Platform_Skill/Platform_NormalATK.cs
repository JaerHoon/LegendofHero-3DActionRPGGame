using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_NormalATK : Platform
{
    bool IsSkillInfo;
    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSkillInfo= true;
            OnSkillInfo();   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSkillInfo= false;
            OffSkillInfo();
        }
    }

    private void Update()
    {
        if (IsSkillInfo && Input.GetKeyDown(KeyCode.E))
        {
            GetSkill();
            IsSkillInfo = false;
            OffSkillInfo();
        }
    }

}
