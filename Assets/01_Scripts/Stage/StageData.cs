using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageDate", menuName = "Scriptable Object/Stage Data")]
public class StageData : ScriptableObject
{
    public int stageNumber;
    public enum StageType { Start, NormalATK, SkillATK, ItemRelic, Market, Boss}
    public StageType stageType;
    public int MinionCount;
    public int WarriorCount;
    public int MageCount;
    public int RogueCount;
    public int GolemCount;
    

    public int MonsterCount()
    {
        int A = MinionCount + WarriorCount + MageCount + RogueCount + GolemCount;

        return A;
    }

}
