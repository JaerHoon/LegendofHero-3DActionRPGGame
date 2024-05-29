using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    ParticleSystem slasher;

    Animator anim;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void usedRay()
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
    }


    public void KnightAttack()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            usedRay();
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            slash.Play();
        }

    }

    public void skillAttack()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            usedRay();
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            slasher.Play();

        }
    }


    // Update is called once per frame
    void Update()
    {
        KnightAttack();
        skillAttack();

    }
}
