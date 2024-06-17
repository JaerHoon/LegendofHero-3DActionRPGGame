using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiceInfo : UIModel
{
    CharacterManager characterManager;

    SkillData knightData;
    SkillData archerData;

    public List<Slot> KnightSlots = new List<Slot>(new Slot[3]);
    public List<Slot> ArcherSlots = new List<Slot>(new Slot[3]);

    private void Awake()
    {
        characterManager = FindFirstObjectByType<CharacterManager>();

        knightData = characterManager.KnightData;
        KnightSlots[0] = new Slot(knightData, 0);
        KnightSlots[1] = new Slot(knightData, 4);
        KnightSlots[2] = new Slot(knightData, 8);


        archerData = characterManager.ArcherData;
        ArcherSlots[0] = new Slot(archerData, 0);
        ArcherSlots[1] = new Slot(archerData, 4);
        ArcherSlots[2] = new Slot(archerData, 8);
    }

    private void Start()
    {
        Invoke("ChangeUI", 1f);
    }

   
}

public class Slot
{
    public Sprite Icon;
    public string Name;
    public string Description;

    public Slot(SkillData data,int skill_Id)
    {
        Icon = data.skillInfo[skill_Id].image;
        Name = data.skillInfo[skill_Id].skillName;
        Description = data.skillInfo[skill_Id].skillDescription;
    }
}
