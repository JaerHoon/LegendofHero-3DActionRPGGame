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
            return; // ��ȣ�� ��ų ������� �� �������� ���� �ʰ� �ϱ� ����.
        }
        //print("�÷��̾� �ǰ� ������ : " + dmg);
        cap.enabled = false; // �ǰ� �� �ݶ��̴��� �� 2������ ��Ȱ��ȭ �Ͽ� �ǰ� �����ð��� ����
        box.enabled = false; // �ǰ� �� �ݶ��̴��� �� 2������ ��Ȱ��ȭ �Ͽ� �ǰ� �����ð��� ����
        heart.SetActive(true); // ��Ʈ ��ƼŬ �����Ű�� �ڵ�
        onOffRendererCoroutine = StartCoroutine(onOffRenderer()); // �ǰ� �� ĳ���� ����Ǵ� �ڷ�ƾ
        Invoke("offSetActive", 2.0f); // 2�� �Ŀ� �ݶ��̴� Ȱ��ȭ

        
        playerHp.fillAmount -= 0.2f; // ĳ���� HP�� 1���� ���� => ��Ʈ�� 5���� 0.2
        
        //�Ҽ��� ������ ���ؼ� 0.8���� 0.2��ŭ ���� �� 0.6�� �ƴ� 0.600001 �̶� ������ �߻��� �� �ִ�
        //���� Mathf.Round�� �̿��Ͽ� �̸� �ݿø� �Ͽ� ���� ����� ������ ����� �ش�.
        //���� ��� 0.60001�� �Ǿ����� 100��ŭ �����༭ 60.001�� ����� Round�� �̿��Ͽ� �ݿø����༭ 60���� �����.
        //�̸� �ٽ� 100���� ������ 0.6���� ������ش�.
        playerHp.fillAmount = Mathf.Round(playerHp.fillAmount * 100f) / 100f;
        
        if(playerHp.fillAmount ==0)
        {
            isPlayerDie = true;
            heart.SetActive(false); // ��ƼŬ ��Ȱ��ȭ �ؼ� �׾��� �� ���̻� ������ �ʰ� �ϱ� ����.
            offNavMesh();
            anim.SetTrigger("Death");
            CoroutineStop(); // �ڷ�ƾ ���ߴ� �Լ�
            cap.enabled = false; // �׾��� �� �ݶ��̴� ��Ȱ��ȭ
            box.enabled = false; // �׾��� �� �ݶ��̴� ��Ȱ��ȭ 
        }
    }

    IEnumerator onOffRenderer()
    {
        float count = 0;
        int childCount = Mathf.Min(transform.childCount, 7); // �������� �ڽĵ� �� 7��°���� �����Ѵ�.

        //�� 2.2�ʵ��� �������� 1~7��° �ڽ� ������Ʈ�� Ȱ��,��Ȱ��ȭ �Ͽ� �ǰݽ� ĳ���Ͱ� ����Ǵ� ����� ����.
        while(count <2.2f)
        {
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false); // 7��° �ڽı��� ��Ȱ��ȭ
            }

            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < childCount; i++)
            { 
                transform.GetChild(i).gameObject.SetActive(true); // 7��° �ڽı��� Ȱ��ȭ
            }

            yield return new WaitForSeconds(0.3f);
            count++;
        }
        
        
    }

    void CoroutineStop()
    {
        if (onOffRendererCoroutine != null)
        {
            StopCoroutine(onOffRendererCoroutine); // �ڷ�ƾ ����
            onOffRendererCoroutine = null;

            int childCount = Mathf.Min(transform.childCount, 7);
            for (int i = 0; i < childCount; i++)
            {
                //�÷��̾� �׾��� �� �����ϴ� �ڽĵ� Ȱ��ȭ ���·� ����
                //���� ���� ���ߴ��� ���� ���¿����� ��� �����ϰ� �־ true �Ǵ� �ڵ� �־���.
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
