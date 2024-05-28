using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    private float damage;//����
    public float Damage { get { return damage; } set { damage = value; } }
    private float cd;//��ٿ�
    public float CD { get { return cd; } set { cd = value; } }
    private float gcd;//�۷ι� ��ٿ�
    public float GCD { get { return gcd; } set { gcd = value; } }
    private float scale;//����
    public float Scale { get { return scale; } set { scale = value; } }
    private float speedRate;//�ӵ� ��ȭ��
    public float SpeedRate { get { return speedRate; } set { speedRate = value; } }
    private float eventRate;//Ȯ��
    public float EventRate { get { return eventRate; } set { eventRate = value; } }
    private int damageCount;//���� Ƚ��
    public int DamageCount { get { return damageCount; } set { damageCount = value; } }
    private int skillCount;//��� Ƚ��
    public int SkillCount { get { return skillCount; } set { skillCount = value; } }
    private int damageRate;//������ ���
    public int DamageRate { get { return damageRate; } set { damageRate = value; } }

    public Skills(float damage, float cd, float gcd, float scale, float speedRate, float eventRate, int damageCount, int skillCount, int damageRate)
    {
        this.damage = damage;
        this.cd = cd;
        this.gcd = gcd;
        this.scale = scale;
        this.speedRate = speedRate;
        this.eventRate = eventRate;
        this.damageCount = damageCount;
        this.skillCount = skillCount;
        this.damageRate = damageRate;
    }


}

