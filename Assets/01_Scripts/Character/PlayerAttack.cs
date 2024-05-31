using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    ParticleSystem shield;
    [SerializeField]
    GameObject skill;
    [SerializeField]
    Transform skillPos;
    [SerializeField]
    float skillPower;

    Animator anim;
    Rigidbody rb;
    PlayerTrigger playerTrigger;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
    }
    /*void usedRay()
    {
        //ScreenPointToRay 함수를 이용해서 마우스 클릭한 위치를 3D 월드 좌표값으로 반환한다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // 레이캐스트 충돌 정보 저장

        // 레이캐스트 이용 => 반환된 ray값을 이용해서 충돌이 발생하면 hit에 충돌 정보 저장
        if (Physics.Raycast(ray, out hit))
        {
            //플레이어가 바라보는 방향값을 설정한다. transform.position.y으로 플레이어가 평면상에서 회전하게 만든다.
            Vector3 lookAtPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //LookAt 함수를 이용하여 lookAtPos 방향값으로 플레이어가 바라보게 만든다.
            transform.LookAt(lookAtPos);
        }
    }*/


    public void KnightAttack() // 기본공격 함수
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            playerTrigger.OnCollider();
            //usedRay();
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            slash.Play();
            slash.transform.localPosition = new Vector3(-0.05f,1.44f,0.87f);
        }

    }

    public void skillAttack() // 스킬 함수
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            //usedRay();
            GameObject swordWave = Instantiate(skill, transform.position, transform.rotation);
            swordWave.transform.position = skillPos.position;
            GameObject swordWave2 = Instantiate(skill, transform.position,transform.rotation);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            //slasher.Play();

        }
    }

    public void block() // 보호막 함수
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("block", true);
            shield.Play();
            StartCoroutine(Endblock());
        }
    }

    IEnumerator Endblock()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("block", false);
        shield.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        KnightAttack();
        skillAttack();
        block();

    }
}
