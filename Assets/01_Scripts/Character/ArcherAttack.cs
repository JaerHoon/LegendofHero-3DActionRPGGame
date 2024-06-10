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

    public bool isButtonPressed1 = false; // 스킬셋팅1번용 플래그
    public bool isButtonPressed2 = false; // 스킬셋팅2번용 플래그
    public bool isButtonPressed3 = false; // 스킬셋팅3번용 플래그

    public bool isAttackButton1 = false; // 기본공격1번용 플래그
    public bool isAttackButton2 = false; // 기본공격2번용 플래그
    public bool isAttackButton3 = false; // 기본공격3번용 플래그
    public bool isShooting = false; // 기본공격3번 함수에 쓰이는 플래그로 연사중일때 움직임 제어를 이용하기 위함
    public bool isCoolTime = false; // 스킬 쿨타임 플래그
    public bool isFreeze = false;
    public bool isBlock = false;
    float lastshotTime; // 기본공격3번 연사할때 제어를 위한 플래그
    float DestroyDuration = 1.5f; // 스킬셋팅 2번에 쓰이는 Destroy 관련 플래그
    float DestroyLifeTime = 2.0f; // 스킬셋팅 2번에 쓰이는 Destroy 관련 플래그

    int skillCount = 0; // 스킬셋팅 3번에 쓰이는 2개 스킬을 보여주기 위한 카운트 변수
    float skillcoolTime = 7.0f; // 임의로 만들어둔 스킬 쿨타임
    ArcherTrigger archerTrigger;
    Arrow arrowTrigger;
    CapsuleCollider cap;
    CharacterDamage die;
    void Start()
    {
        anim = GetComponent<Animator>();
        archerTrigger = GetComponent<ArcherTrigger>();
        arrowTrigger = GetComponent<Arrow>();
        cap = GetComponent<CapsuleCollider>();
        die = GetComponent<CharacterDamage>();

    }

    void shotArrow() // 기본공격 할 때 화살 생성 및 위치를 구현한 함수
    {
        GameObject arrowShot = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot.transform.position = arrowPos.position;
    }

    public void ArrowAttack() // 궁수 캐릭터의 기본공격을 위한 함수
    {
        if (Input.GetMouseButtonDown(0) && !isAttackButton3) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            // 공격 애니메이션에 맞춰서 화살을 나가게 하기 위해 0.3초의 딜레이를 줌
            Invoke("shotArrow", 0.3f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행

            if(isAttackButton1) // 기본공격 1번 셋팅
            {
                Invoke("AttackSetting1", 0.3f);
            }
            else if(isAttackButton2) // 기본공격 2번 셋팅
            {
                Invoke("AttackSetting2", 0.3f);
            }
            
        }
        
        //기본공격 3번 셋팅을 위한 if문
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

    void AttackSetting1() // 기본공격 1번 셋팅으로 ↑↑ 모양으로 화살이 발사됨.
    {
        GameObject arrowShot2 = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot2.transform.position = arrowPos2.position;
    }

    void AttackSetting2() // 기본공격 2번 셋팅으로 누르면 화살 한발이 연이어서 나간다.
    {
        StartCoroutine(repeatingArrow());
    }
    IEnumerator repeatingArrow()
    {
        yield return new WaitForSeconds(0.15f);
        shotArrow();
    }


    void AttackSetting3() // 기본공격3번으로 마우스 왼쪽을 누르고 있는 동안 일정 간격으로 화살이 계속 발사됨.
    {
       if(Time.time - lastshotTime >= 0.2f)
        {
            shotArrow();
            lastshotTime = Time.time;
        }
    }

    
    public void skillAttack() // 스킬을 발동하기 위한 함수 => 마우스 오른쪽 버튼을 누르면 스킬이 발동된다.
    {
        if (Input.GetMouseButtonDown(1) && !isCoolTime) // 마우스 오른쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            //공격 애니메이션과 싱크를 어느정도 맞추기 위해서 0.4초간의 딜레이를 줌
            Invoke("usedRay", 0.4f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("skillAttack");
            
            if(isButtonPressed1) // 스킬셋팅 1번을 사용하기 위한 조건
            {
                //skillSetting1();
                isFreeze = true;
                Debug.Log("빙결적용!\n 몬스터의 이동속도가 감소합니다!");
                StartCoroutine(offFreeze());
            }
            else if(isButtonPressed2) // 스킬셋팅 2번을 사용하기 위한 조건
            {
                skillSetting2();
            }
        }
        //스킬셋팅 3번을 사용하기 위한 조건
        if(Input.GetMouseButtonDown(1) && isButtonPressed3 && !isCoolTime)
        {
            skillSetting3();
        }

    }

    void skillSetting1() // 스킬셋팅 1번 함수
    {
        // 10% 확률로 디버프를 거는 함수이다.
        float Debuff = Random.Range(0f, 100f);
        if (Debuff < 10.0f)
        {
            isFreeze = true;
            Debug.Log("빙결적용!\n 몬스터의 이동속도가 감소합니다!");
            StartCoroutine(offFreeze());
        }
    }

    IEnumerator offFreeze()
    {
        yield return new WaitForSeconds(10.0f);
        isFreeze = false;
    }

    void skillSetting2() // 스킬셋팅 2번 함수
    {
        ArrowRainParticle.instance.ParticleControl();

        var emi = ArrowRain.emission;
        var bur = new ParticleSystem.Burst[emi.burstCount];
        emi.GetBursts(bur);

        for (int i = 0; i < bur.Length; i++)
        {
            bur[i].count = 4;
        }

        emi.SetBursts(bur);
    }
    void skillSetting3() // 스킬셋팅 3번 함수
    {
        if(isCoolTime == true)
        {
            return;
        }
    
        
        skillCount++;
        if(skillCount >=2)
        {
            StartCoroutine(coolTimeStart());
        }
    }

    IEnumerator coolTimeStart()
    {
        isCoolTime = true;
        Debug.Log("쿨타임 7초 시작");
        yield return new WaitForSeconds(skillcoolTime);

        isCoolTime = false;
        skillCount = 0;
        Debug.Log("스킬을 사용할 수 있습니다");
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
            
            if(isButtonPressed2)
            {
                Destroy(ps.gameObject, DestroyDuration + DestroyLifeTime);
            }
            else
            {
                //파티클이 생성되고 마지막 파티클이 소멸되면 파티클이 들어가 있는 게임오브젝트를 Destroy한다.
                //예시로 duration =2초, startLifetime= 0.5초로 설정했다면 2.5초뒤에 Destroy한다는 뜻이다.
                Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            
        }
    }


    public void block() // 궁수 보호막 스킬을 사용하기 위한 함수
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBlock = true;
            magicShield.Play();
            magicShield.transform.position = shieldPos.position;
            cap.enabled = false;
            StartCoroutine(Endblock());
        }
    }

    IEnumerator Endblock()
    {
        yield return new WaitForSeconds(1.5f);
        isBlock = false;
        magicShield.Stop();
        cap.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "MagicBullet" && isBlock == true)
        {
            Destroy(other.gameObject);
        }
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
        isButtonPressed1 = !isButtonPressed1;
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
        if(die.isPlayerDie == true)
        {
            return;
        }
        
        ArrowAttack();
        skillAttack();
        block();
    }
}
