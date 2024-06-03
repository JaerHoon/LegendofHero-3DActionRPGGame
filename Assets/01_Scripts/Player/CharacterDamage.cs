using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
   public virtual void OnDamage(float dmg)
    {
        print("플레이어 피격 데미지 : " + dmg);
    }
}
