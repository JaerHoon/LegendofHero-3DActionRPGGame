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

    protected Character character;


    
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

       
    }
    public virtual void OnDamage(float dmg)
    {
        if(PlayerAttack.instance != null && PlayerAttack.instance.isBlock==true ||
           ArcherAttack.instance !=null && ArcherAttack.instance.isBlock == true)
        {
            return; // 보호막 스킬 사용중일 때 데미지를 입지 않게 하기 위함.
        }
        //print("플레이어 피격 데미지 : " + dmg);
        cap.enabled = false; // 피격 시 콜라이더를 약 2초정도 비활성화 하여 피격 무적시간을 구현
        box.enabled = false; // 피격 시 콜라이더를 약 2초정도 비활성화 하여 피격 무적시간을 구현
        heart.SetActive(true); // 하트 파티클 재생시키는 코드
        onOffRendererCoroutine = StartCoroutine(onOffRenderer()); // 피격 시 캐릭터 점멸되는 코루틴
        Invoke("offSetActive", 2.0f); // 2초 후에 콜라이더 활성화
        character.PlayerHp--;
     
    }

    protected IEnumerator onOffRenderer()
    {
        float count = 0;
        int childCount = Mathf.Min(transform.childCount, 7); // 워리어의 자식들 중 7번째까지 포함한다.

        //약 2.2초동안 워리어의 1~7번째 자식 오브젝트를 활성,비활성화 하여 피격시 캐릭터가 점멸되는 모습을 구현.
        while(count <2.2f)
        {
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false); // 7번째 자식까지 비활성화
            }

            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < childCount; i++)
            { 
                transform.GetChild(i).gameObject.SetActive(true); // 7번째 자식까지 활성화
            }

            yield return new WaitForSeconds(0.3f);
            count++;
        }
        
        
    }

    protected void CoroutineStop()
    {
        if (onOffRendererCoroutine != null)
        {
            StopCoroutine(onOffRendererCoroutine); // 코루틴 멈춤
            onOffRendererCoroutine = null;

            int childCount = Mathf.Min(transform.childCount, 7);
            for (int i = 0; i < childCount; i++)
            {
                //플레이어 죽었을 때 점멸하던 자식들 활성화 상태로 유지
                //따로 설정 안했더니 죽은 상태에서도 계속 점멸하고 있어서 true 되는 코드 넣어줌.
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

    
}
