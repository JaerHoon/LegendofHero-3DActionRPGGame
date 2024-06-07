using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    GameObject heart;
 
    public virtual void OnDamage(float dmg)
    {
        print("�÷��̾� �ǰ� ������ : " + dmg);
        heart.SetActive(true);
        Invoke("offSetActive", 1.5f);
    }

    void offSetActive()
    {
        heart.SetActive(false);
    }

    
}
