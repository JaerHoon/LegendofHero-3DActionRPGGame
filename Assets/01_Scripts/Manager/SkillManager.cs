using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public List<Skills> WarriorSkills = new List<Skills>();
    public List<Skills> ArcherSkills = new List<Skills>();

    private void Start()
    {
        Skills wAttack = new Skills(220f, 0f, 1.5f, 1f, 1f, 1f, 1, 1, 1);//0~3 평타
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(400f, 0f, 2f, 1.5f, 0.7f, 1f, 1, 1, 1);
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(220f, 0f, 1.1f, 1f, 1f, 1f, 1, 1, 1);
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(110f, 0f, 1.5f, 1f, 1f, 1f, 3, 1, 1);
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(60f, 0f, 1.5f, 1f, 1f, 1f, 1, 1, 1);//4~7 스킬
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(0f, 0f, 0.5f, 1f, 1.2f, 1f, 1, 1, 1);
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(220, 0f, 1.5f, 1.5f, 1f, 1f, 1, 1, 1);
        WarriorSkills.Add(wAttack);
        wAttack = new Skills(0f, 5f, 1.5f, 1f, 0f, 1f, 1, 1, 1);
        WarriorSkills.Add(wAttack);

        Skills aAttack = new Skills(100f, 0f, 1f, 1f, 1f, 1f, 1, 1, 1);//0~3 평타
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(300f, 4f, 1f, 1f, 1f, 1f, 1, 1, 1);
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(80f, 0f, 1f, 1f, 1f, 1f, 1, 3, 1);
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(100f, 0f, 1f, 1f, 1f, 1f, 1, 1, 1);
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(90, 8f, 1.5f, 1f, 1f, 1f, 3, 3, 1);//4~7 스킬
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(200, 8f, 2f, 1f, 1f, 1f, 2, 3, 1);
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(110, 8f, 1.5f, 1f, 1f, 0.1f, 3, 3, 1);
        ArcherSkills.Add(aAttack);
        aAttack = new Skills(110, 10f, 1.8f, 1f, 1f, 1f, 3, 3, 1);
        ArcherSkills.Add(aAttack);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
