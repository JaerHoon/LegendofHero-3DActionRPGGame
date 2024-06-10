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
    BoxCollider box;
    public bool isPlayerDie = false;
    Coroutine onOffRendererCoroutine;
    private void Start()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        box = GetComponent<BoxCollider>();
    }
    public virtual void OnDamage(float dmg)
    {
        if(PlayerAttack.instance != null && PlayerAttack.instance.isBlock==true ||
           ArcherAttack.instance !=null && ArcherAttack.instance.isBlock == true)
        {
            return;
        }
        //print("�÷��̾� �ǰ� ������ : " + dmg);
        cap.enabled = false;
        box.enabled = false;
        heart.SetActive(true);
        onOffRendererCoroutine = StartCoroutine(onOffRenderer());
        Invoke("offSetActive", 2.0f);

        
        playerHp.fillAmount -= 0.2f;
        //�Ҽ��� ������ ���ؼ� 0.8���� 0.2��ŭ ���� �� 0.6�� �ƴ� 0.600001 �̶� ������ �߻��� �� �ִ�
        //���� Mathf.Round�� �̿��Ͽ� �̸� �ݿø� �Ͽ� ���� ����� ������ ����� �ش�.
        //���� ��� 0.60001�� �Ǿ����� 100��ŭ �����༭ 60.001�� ����� Round�� �̿��Ͽ� �ݿø����༭ 60���� �����.
        //�̸� �ٽ� 100���� ������ 0.6���� ������ش�.
        playerHp.fillAmount = Mathf.Round(playerHp.fillAmount * 100f) / 100f;
        
        if(playerHp.fillAmount ==0)
        {
            isPlayerDie = true;
            heart.SetActive(false);
            offNavMesh();
            anim.SetTrigger("Death");
            CoroutineStop();
            cap.enabled = false;
            box.enabled = false;
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

    void CoroutineStop()
    {
        if (onOffRendererCoroutine != null)
        {
            StopCoroutine(onOffRendererCoroutine);
            onOffRendererCoroutine = null;

            int childCount = Mathf.Min(transform.childCount, 7);
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
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
        box.enabled = true;
    }

}
