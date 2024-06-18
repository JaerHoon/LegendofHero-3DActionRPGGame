using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    protected GameObject heart;
    [SerializeField]
    Image playerHp;

    [SerializeField]
    GameObject panel;

    protected Character character;


    Image image;
    
    protected Animator anim;
    protected CapsuleCollider cap;
    protected BoxCollider box;
    public bool isPlayerDie = false;
    protected Coroutine onOffRendererCoroutine;
    private void Start()
    {
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        box = GetComponent<BoxCollider>();

        image = GameObject.Find("GameOverPanel").GetComponent<Image>();
        SetImageAlpha(0f);
        panel.SetActive(false);
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


        
        if(playerHp.fillAmount ==0)
        {
            isPlayerDie = true;
            panelOnOff();
            heart.SetActive(false); // ��ƼŬ ��Ȱ��ȭ �ؼ� �׾��� �� ���̻� ������ �ʰ� �ϱ� ����.
            //offNavMesh();
            anim.SetTrigger("Death");
            CoroutineStop(); // �ڷ�ƾ ���ߴ� �Լ�
        }
    }

    protected IEnumerator onOffRenderer()
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

    protected void CoroutineStop()
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

    /*void offNavMesh()
    {
        NavMeshAgent[] monsterNav = FindObjectsOfType<NavMeshAgent>();
        foreach(NavMeshAgent agent in monsterNav)
        {
            if(agent.CompareTag("Monster"))
            {
                agent.enabled = false;
            }
        }
    }*/

    protected void offSetActive()
    {
        if(isPlayerDie==true)
        {
            return;
        }
        
        heart.SetActive(false);
        cap.enabled = true;
        box.enabled = true;
    }

    void panelOnOff()
    {
        panel.SetActive(true);
        StartCoroutine(onoffPanel());
        
    }

    IEnumerator onoffPanel()
    {
        float dur = 1.0f;
        float time = 0f;
        float target = 250.0f / 255.0f;

        while (time < dur)
        {
            time += Time.deltaTime;
            float Alpha = Mathf.Clamp01(time / dur);
            SetImageAlpha(Alpha);
            yield return null;
            

        }
        SetImageAlpha(target);
        yield break;

    }

    void SetImageAlpha(float alpha)
    {
        if(image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
