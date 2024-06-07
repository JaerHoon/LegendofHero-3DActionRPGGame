using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    GameObject heart;
 
    public virtual void OnDamage(float dmg)
    {
        print("플레이어 피격 데미지 : " + dmg);
        heart.SetActive(true);
        Invoke("offSetActive", 1.5f);
    }

    void offSetActive()
    {
        heart.SetActive(false);
    }

    
}
