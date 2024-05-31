using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    public static ArcherAttack instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [SerializeField]
    GameObject arrow; // 화살 오브젝트
    [SerializeField]
    ParticleSystem ArrowRain; // 궁수 스킬 파티클
    [SerializeField]
    ParticleSystem magicShield; // 궁수 보호막 파티클
    [SerializeField]
    Transform arrowPos; // 화살 발사되는 위치
    [SerializeField]
    Transform shieldPos; // 보호막 생성 위치

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void shotArrow()
    {
        GameObject arrowShot = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot.transform.position = arrowPos.position;
    }

    public void ArrowAttack()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            //usedRay();
            Invoke("shotArrow", 0.3f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            
        }

    }

    public void skillAttack()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            Invoke("usedRay", 0.4f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("skillAttack");
            

        }
    }

    void usedRay()
    {
        //ScreenPointToRay 함수를 이용해서 마우스 클릭한 위치를 3D 월드 좌표값으로 반환한다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // 레이캐스트 충돌 정보 저장

        // 레이캐스트 이용 => 반환된 ray값을 이용해서 충돌이 발생하면 hit에 충돌 정보 저장
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 objPos = hit.point; // 파티클 재생시킬 위치값을 넣어준다.
            ParticleSystem ps = Instantiate(ArrowRain, objPos, transform.rotation); // 클릭한 위치에 생성될 파티클을 넣어준다.
            ps.Play(); // 파티클 시스템을 재생시킨다.
            //파티클이 생성되고 마지막 파티클이 소멸되면 파티클이 들어가 있는 게임오브젝트를 Destroy한다.
            //예시로 duration =2초, startLifetime= 0.5초로 설정했으므로 2.5초뒤에 Destroy한다.
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }


    public void block()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            magicShield.Play();
            magicShield.transform.position = shieldPos.position;
            StartCoroutine(Endblock());
        }
    }

    IEnumerator Endblock()
    {
        yield return new WaitForSeconds(1.5f);
        magicShield.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ArrowAttack();
        skillAttack();
        block();

    }
}
