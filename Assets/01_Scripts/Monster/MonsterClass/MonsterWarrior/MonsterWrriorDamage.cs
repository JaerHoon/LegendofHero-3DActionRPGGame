using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWrriorDamage : MonsterDamage
{
    private void Start()
    {
        Init();
    }
    public override void OnDamage(float pow)
    {
        monster.DetectedPlayer();
       
        monster.CurrenHP -= pow + (pow * UpDamage / 100);
        DamageFx(pow);
    }
}
