using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTrigger : MonoBehaviour
{
    BoxCollider box;
    MonsterBuff Freeze;
    Archer archerController;
    Inventory inventroy;
    void Start()
    {
        box = GetComponent<BoxCollider>();
        archerController = GameObject.FindWithTag("Player").GetComponent<Archer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            other.GetComponent<MonsterDamage>().OnDamage(archerController.playerSkillsSlot[1].damage);
            CharacterSound.instance.OnArcherSkillHitSound();
            //Freeze.GetComponent<MonsterBuff>().Onfreeze(10.0f, 1.0f, 1.0f);
            inventroy = GameObject.FindFirstObjectByType<Inventory>();

            foreach (var item in inventroy.invenItems)
            {
                if (item.itemID == 5)
                {
                    other.GetComponent<MonsterBuff>().OnPoison(5.0f, 1.0f, 5.0f);
                }
            }


            if (SkillManager.instance.gainedSkill_Archer[1].id==5)
            {
                
                // 10% 확률로 디버프를 거는 함수이다.
                float Debuff = Random.Range(0f, 100f);
                if (Debuff < 10.0f)
                {
                    other.GetComponent<MonsterBuff>().Onfreeze(5.0f, 1f, 20.0f);
                }
                
            }
        }        
    }

    public void archerOnCollider()
    {
        Invoke("archeroffCollider", 0.2f);
        Invoke("archerOnColliders" ,0.1f);

        if(ArcherAttack.instance != null && ArcherAttack.instance.isSkillsSetting2)
        {
            Invoke("archeroffCollider", 0.4f * Time.deltaTime);
            Invoke("archerOnColliders", 0.2f * Time.deltaTime);
        }

       
        
    }

    
    void archerOnColliders()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    void archeroffCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        archerOnCollider();
        
    }
}
