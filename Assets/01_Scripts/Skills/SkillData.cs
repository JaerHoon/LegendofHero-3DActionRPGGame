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
    private int ID;//��ų ���̵�
    public int id { get { return ID; } }

    [SerializeField]
    private string SkillDescription;//��ų ���̵�
    public string skillDescription { get { return SkillDescription; } }

    [SerializeField]
    private Sprite Image;//��ų �̹���
    public Sprite image { get { return Image; } }

    [SerializeField]
    private float Damage;//����
    public float damage { get { return Damage; } }

    [SerializeField]
    private float CD;//��ٿ�
    public float cd { get { return CD; } }

    [SerializeField]
    private float GCD;//�۷ι� ��ٿ�
    public float gcd { get { return GCD; } }

    [SerializeField]
    private float Scale;//����
    public float scale { get { return Scale; } }

    [SerializeField]
    private float SpeedRate;//�ӵ� ��ȭ��
    public float speedRate { get { return SpeedRate; } }

    [SerializeField]
    private float EventRate;//Ȯ��
    public float eventRate { get { return EventRate; } }

    [SerializeField]
    private int DamageCount;//���� Ƚ��
    public int damageCount { get { return DamageCount; } }

    [SerializeField]
    private int SkillCount;//��� Ƚ��
    public int skillCount { get { return SkillCount; } }

    [SerializeField]
    private float DamageRate;//������ ���
    public float damageRate { get { return DamageRate; } }


}