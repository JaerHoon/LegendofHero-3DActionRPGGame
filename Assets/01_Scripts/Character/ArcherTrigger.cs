using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTrigger : MonoBehaviour
{
    BoxCollider box;
    MonsterBuff Freeze;
    Archer archerController;
    bool isFreeze = false;
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
            //Freeze.GetComponent<MonsterBuff>().Onfreeze(10.0f, 1.0f, 1.0f);
            if(ArcherAttack.instance.isButtonPressed1==true && !isFreeze)
            {
                // 10% 확률로 디버프를 거는 함수이다.
                float Debuff = Random.Range(0f, 100f);
                if (Debuff < 10.0f)
                {
                    other.GetComponent<Freeze>().OnBuff(10.0f, 1f, 20f);
                    Debug.Log("빙결적용");
                    StartCoroutine(FrezzeOnOff());

                }
                
            }
        }        
    }

    IEnumerator FrezzeOnOff()
    {
        isFreeze = true;
        yield return new WaitForSeconds(10.0f);
        isFreeze = false;
    }

    public void archerOnCollider()
    {
        Invoke("archeroffCollider", 0.2f);
        Invoke("archerOnColliders" ,0.1f);

        if(ArcherAttack.instance != null && ArcherAttack.instance.isButtonPressed2)
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
