using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SKillDate", menuName = "Scriptable Object/Skill Data", order = int.MinValue)]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private string ClassType;
    public string classType { get { return ClassType; } set { ClassType = value; } }

    [SerializeField]
    SkillInfo[] SkillInfo;
    public SkillInfo[] skillInfo { get { return SkillInfo; } }
}


[System.Serializable]
public struct SkillInfo
{
    [SerializeField]
    private int ID;//스킬 아이디
    public int id { get { return ID; } }

    [SerializeField]
    private string SkillDescription;//스킬 아이디
    public string skillDescription { get { return SkillDescription; } }

    [SerializeField]
    private Sprite Image;//스킬 이미지
    public Sprite image { get { return Image; } }

    [SerializeField]
    private float Damage;//위력
    public float damage { get { return Damage; } }

    [SerializeField]
    private float CD;//쿨다운
    public float cd { get { return CD; } }

    [SerializeField]
    private float GCD;//글로벌 쿨다운
    public float gcd { get { return GCD; } }

    [SerializeField]
    private float Scale;//범위
    public float scale { get { return Scale; } }

    [SerializeField]
    private float SpeedRate;//속도 변화율
    public float speedRate { get { return SpeedRate; } }

    [SerializeField]
    private float EventRate;//확률
    public float eventRate { get { return EventRate; } }

    [SerializeField]
    private int DamageCount;//공격 횟수
    public int damageCount { get { return DamageCount; } }

    [SerializeField]
    private int SkillCount;//사용 횟수
    public int skillCount { get { return SkillCount; } }

    [SerializeField]
    private float DamageRate;//데미지 배수
    public float damageRate { get { return DamageRate; } }


}