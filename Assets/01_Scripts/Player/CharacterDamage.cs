using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
   public virtual void OnDamage(float dmg)
    {
        print("�÷��̾� �ǰ� ������ : " + dmg);
    }
}
