using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    //[SerializeField]
    //ScriptableObject playerDamage;
    Warrior controller;
    
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        controller = GameObject.FindWithTag("Player").GetComponent<Warrior>();
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //몬스터 공격했을 때 데미지를 주는 코드
            other.GetComponent<MonsterDamage>().OnDamage(controller.playerSkillsSlot[0].damage * ItemManager.instance.itemToAttackDamageRate);

            // 2번째 스킬 골랐을 때 10% 확률로 중독 상태이상 거는 코드
            if(PlayerAttack.instance.isAttackSetting2 == true)
            {
                float Debuff = Random.Range(0f, 100f);
                if (Debuff < 10.0f) // 10% 확률로 중독 상태이상을 건다.
                {
                    Debug.Log("중독상태이상 적용되었습니다!");
                    other.GetComponent<MonsterBuff>().OnPoison(5, 1, 50); // 1초당 50의 중독 데미지를 5초동안 준다.
                }
                
            }
            
            if(PlayerAttack.instance.isBlock==true)
            {
                CharacterSound.instance.OnKnightShieldSound();
            }

        }
        else if (other.gameObject.tag == "Dummy" )
        {
            other.GetComponent<Dummy>().OnHit(controller.playerSkillsSlot[0].damage);
        }

    }

    public void OnCollider()
    {
        Invoke("OnColliders", 0.2f);
        Invoke("offCollider", 0.3f);
    }

    void OnColliders()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    void offCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
