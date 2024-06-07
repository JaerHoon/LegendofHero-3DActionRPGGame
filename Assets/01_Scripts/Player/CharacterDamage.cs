using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    GameObject heart;
    [SerializeField]
    Image playerHp;
  

    Animator anim;
    CapsuleCollider cap;
    public bool isPlayerDie = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
    }
    public virtual void OnDamage(float dmg)
    {
        //print("플레이어 피격 데미지 : " + dmg);
        cap.enabled = false;
        heart.SetActive(true);
        StartCoroutine(onOffRenderer());
        Invoke("offSetActive", 2.0f);

        
        playerHp.fillAmount -= 0.2f;
        //소수점 오차로 인해서 0.8에서 0.2만큼 깎였을 때 0.6이 아닌 0.600001 이란 오차가 발생할 수 있다
        //따라서 Mathf.Round를 이용하여 이를 반올림 하여 가장 가까운 정수로 만들어 준다.
        //예를 들면 0.60001이 되었을때 100만큼 곱해줘서 60.001를 만들고 Round를 이용하여 반올림해줘서 60으로 만든다.
        //이를 다시 100으로 나눠서 0.6으로 만들어준다.
        playerHp.fillAmount = Mathf.Round(playerHp.fillAmount * 100f) / 100f;
        if(playerHp.fillAmount ==0)
        {
            isPlayerDie = true;
            offNavMesh();
            anim.SetTrigger("Death");
        }
    }

    IEnumerator onOffRenderer()
    {
        float count = 0;
        int childCount = Mathf.Min(transform.childCount, 7);
        
        while(count <2.2f)
        {
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(0.3f);
            count++;
        }
        
    }

    void offNavMesh()
    {
        NavMeshAgent[] monsterNav = FindObjectsOfType<NavMeshAgent>();
        foreach(NavMeshAgent agent in monsterNav)
        {
            if(agent.CompareTag("Monster"))
            {
                agent.enabled = false;
            }
        }
    }

    void offSetActive()
    {
        heart.SetActive(false);
        cap.enabled = true;
    }
}
