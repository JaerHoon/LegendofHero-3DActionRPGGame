using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    private float damage;//위력
    public float Damage { get { return damage; } set { damage = value; } }
    private float cd;//쿨다운
    public float CD { get { return cd; } set { cd = value; } }
    private float gcd;//글로벌 쿨다운
    public float GCD { get { return gcd; } set { gcd = value; } }
    private float scale;//범위
    public float Scale { get { return scale; } set { scale = value; } }
    private float speedRate;//속도 변화율
    public float SpeedRate { get { return speedRate; } set { speedRate = value; } }
    private float eventRate;//확률
    public float EventRate { get { return eventRate; } set { eventRate = value; } }
    private int damageCount;//공격 횟수
    public int DamageCount { get { return damageCount; } set { damageCount = value; } }
    private int skillCount;//사용 횟수
    public int SkillCount { get { return skillCount; } set { skillCount = value; } }
    private int damageRate;//데미지 배수
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

