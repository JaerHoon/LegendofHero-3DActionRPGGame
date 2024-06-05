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
    Transform arrowPos2; // 듀얼화살용 발사 위치
    [SerializeField]
    Transform shieldPos; // 보호막 생성 위치

    Animator anim;

    public bool isButtonPressed = false;
    public bool isButtonPressed2 = false;
    public bool isButtonPressed3 = false;

    public bool isAttackButton1 = false;
    public bool isAttackButton2 = false;
    public bool isAttackButton3 = false;
    public bool isShooting = false;
    float lastshotTime;
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
        if (Input.GetMouseButtonDown(0) && !isAttackButton3) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            Invoke("shotArrow", 0.3f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행

            if(isAttackButton1)
            {
                Invoke("AttackSetting1", 0.3f);
            }
            else if(isAttackButton2)
            {
                Invoke("AttackSetting2", 0.3f);
            }
            
        }
        
        if(Input.GetMouseButton(0) && isAttackButton3)
        {
            isShooting = true;
            anim.SetBool("holdAttack", true);
            AttackSetting3();
        }
        else
        {
            isShooting = false;
            anim.SetBool("holdAttack", false);
        }
    }

    void AttackSetting1()
    {
        GameObject arrowShot2 = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot2.transform.position = arrowPos2.position;
    }

    void AttackSetting2()
    {
        StartCoroutine(repeatingArrow());
    }
    IEnumerator repeatingArrow()
    {
        yield return new WaitForSeconds(0.15f);
        shotArrow();
    }


    void AttackSetting3()
    {
       if(Time.time - lastshotTime >= 0.2f)
        {
            shotArrow();
            lastshotTime = Time.time;
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

    //**********버튼 동작을 위한 함수**********//  
    public void OnArcherAttackButton_First()
    {
        isAttackButton1 = !isAttackButton1;
    }

    public void OnArcherAttackButton_Second()
    {
        isAttackButton2 = !isAttackButton2;
    }

    public void OnArcherAttackButton_Third()
    {
        isAttackButton3 = !isAttackButton3;

        
    }

    public void OnButtonArcherSkill_First()
    {
        isButtonPressed = !isButtonPressed;
    }

    public void OnButtonArcherSkill_Second()
    {
        isButtonPressed2 = !isButtonPressed2;

    }

    public void OnButtonArcherSkill_Third()
    {
        isButtonPressed3 = !isButtonPressed3;
    }

    // Update is called once per frame
    void Update()
    {
        ArrowAttack();
        skillAttack();
        block();

    }
}
