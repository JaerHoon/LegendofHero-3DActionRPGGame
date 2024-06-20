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
            //���� �������� �� �������� �ִ� �ڵ�
            other.GetComponent<MonsterDamage>().OnDamage(controller.playerSkillsSlot[0].damage * ItemManager.instance.itemToAttackDamageRate);

            // 2��° ��ų ����� �� 10% Ȯ���� �ߵ� �����̻� �Ŵ� �ڵ�
            if(PlayerAttack.instance.isAttackSetting2 == true)
            {
                float Debuff = Random.Range(0f, 100f);
                if (Debuff < 10.0f) // 10% Ȯ���� �ߵ� �����̻��� �Ǵ�.
                {
                    Debug.Log("�ߵ������̻� ����Ǿ����ϴ�!");
                    other.GetComponent<MonsterBuff>().OnPoison(5, 1, 50); // 1�ʴ� 50�� �ߵ� �������� 5�ʵ��� �ش�.
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
