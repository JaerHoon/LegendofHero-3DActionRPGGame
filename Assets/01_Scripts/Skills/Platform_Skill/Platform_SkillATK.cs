using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_SkillATK : Platform
{
    bool IsSkillInfo;
    void Start()
    {
        Init();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnSkillInfo();
            IsSkillInfo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OffSkillInfo();
            IsSkillInfo = false;
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
