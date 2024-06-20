using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    ParticleSystem chopSlash;
    [SerializeField]
    ParticleSystem shield;
    [SerializeField]
    GameObject skill;
    [SerializeField]
    GameObject skill_red;
    [SerializeField]
    Transform skillPos;
    [SerializeField]
    Transform skillPos2;
    [SerializeField]
    Transform skillPos3;
    [SerializeField]
    float skillPower;
    [SerializeField]
    ParticleSystem waveEX;
    Animator anim;
    PlayerTrigger playerTrigger;
    public bool isButtonPressed = false;
    public bool isButtonPressed2 = false;
    public bool isSkillSetting3 = false;
    bool isAttackButton1 = false;
    public bool isAttackSetting2 = false;
    public bool isBlock = false;
    public bool isAttackButton3 = false;
    GameObject swordWave;
    CharacterDamage die;
    CapsuleCollider cap;
    Warrior controller;

    //[SerializeField]
    //AudioStorage soundStorage;

    //AudioSource myaudio;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        //myaudio = GetComponent<AudioSource>();
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
        die = GetComponent<CharacterDamage>();
        cap = GetComponent<CapsuleCollider>();
        controller = GameObject.FindWithTag("Player").GetComponent<Warrior>();
        isBlock = false;
        
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
        if (die.isPlayerDie == true) // 플레이어 사망 상태일 때 동작에 제한을 두기 위함.
        {
            return;
        }

        switch (SkillManager.instance.gainedSkill_Warrior[0].id)
        {
            case 0:
                baseAttack(); // 기본공격
                break;
            case 1:
                AttackSetting1(); // 기본공격 강화 1번셋팅
                break;
            case 2:
                AttackSetting2(); // 기본공격 강화 2번셋팅
                break;
            case 3:
                AttackSetting3(); // 기본공격 강화 3번셋팅
                break;
        }
    }
    void baseAttack()
    {
        
        playerTrigger.OnCollider();
        //마우스 클릭시 공격 애니메이션이 발동된다.
        anim.SetTrigger("Attack");
        //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
        slash.Play();
        CharacterSound.instance.OnKnightBaseAttackSound();
        //가끔 슬래쉬 위치가 바뀌는 경우가 있어서 적절한 위치값을 넣어줘서 고정시켜 준다.
        slash.transform.localPosition = new Vector3(-0.05f, 1.44f, 0.87f);
    }
    public void AttackSetting1()
    {
        controller.OnChangeSkills(1);
        //평타강화1 버전 => 범위를 늘려준다.
        slash.transform.localScale = new Vector3(1.7f, 1.0f, 1.7f);
        baseAttack();
    }
    void AttackSetting2()
    {
        controller.OnChangeSkills(2);
        isAttackSetting2 = true;
        baseAttack();
        var mainColor = slash.main;
        mainColor.startColor = Color.magenta; // 저주or독 이미지를 생각해서 평타강화2를 누르면 슬래쉬가 보라색으로 변한다.
        
    }
    void AttackSetting3()
    {
        controller.OnChangeSkills(3);
        //baseAttack();
        //평타강화3 버전으로 횡베기후에 내려찍는 모션으로 연속공격을 한다.
        playerTrigger.OnCollider();
        anim.SetBool("isAttack",true);
        slash.Play(); // 횡베기 했을 때 슬래쉬 나오게 파티클 재생
        StartCoroutine(chopslashPlay());
    }
    IEnumerator chopslashPlay()
    {
        yield return new WaitForSeconds(0.4f);
        // 코루틴 통해서 0.4초뒤에 내려찍을 때 슬래쉬 파티클을 재생한다.
        // 내려찍을 때 슬래쉬 나오는 타이밍을 맞추기 위해서 코루틴을 이용한다.
        playerTrigger.OnCollider();
        chopSlash.Play();
        //내려찍는 슬래쉬의 적절한 위치값을 넣어줘서 엉뚱한 위치에서 재생되지 않게 고정시켜 준다.
        chopSlash.transform.localPosition = new Vector3(0.9f, 2.4f, 1.5f);
    }
    public void OnAttack2End() // 연속공격 이후에 idle로 돌아가는 상태이며 애니메이션 이벤트로 동작된다.
    {
        anim.SetBool("isAttack", false);
    }
    public void skillAttack() // 스킬 함수
    {
        if (die.isPlayerDie == true) // 플레이어 사망 상태일 때 동작에 제한을 두기 위함.
        {
            return;
        }
        switch (SkillManager.instance.gainedSkill_Warrior[1].id)
        {
            case 4:
                skillBaseAttack(); // 기본공격
                break;
            case 5:
                skillsetting1(); // 스킬 강화 1번셋팅
                break;
            case 6:
                skillsetting2(); // 스킬 강화 2번셋팅
                break;
            case 7:
                skillsetting3(); // 스킬 강화 3번셋팅
                break;
        }
    }
    void skillBaseAttack()
    {
        anim.SetTrigger("Attack");
        //검기 스킬 발사하도록 Instantiate 이용한다.
        swordWave = Instantiate(skill, transform.position, transform.rotation);
        CharacterSound.instance.OnKnightSkillSound();
        // 캐릭터 앞 쪽에 위치시켜서 이상한 곳에서 안나오도록 고정시키기 위함이다.
        swordWave.transform.position = skillPos.position;
    }
    void skillsetting1()
    {
        controller.OnChangeSkills(5);
        skillBaseAttack();
        //스킬강화1 버전으로 ↖↑↗ 방향으로 나아가기 위해 ↖↗ 방향으로 나가는 검기들만 따로 설정해 주었다.
        Quaternion WaveRot2 = transform.rotation * Quaternion.Euler(0, 50.0f, 0);
        Quaternion WaveRot3 = transform.rotation * Quaternion.Euler(0, -50.0f, 0);
        GameObject swordWave2 = Instantiate(skill, transform.position, WaveRot2);
        GameObject swordWave3 = Instantiate(skill, transform.position, WaveRot3);
        swordWave2.transform.position = skillPos2.position;
        swordWave3.transform.position = skillPos3.position;
    }
    void skillsetting2()
    {
        controller.OnChangeSkills(6);
        skillBaseAttack();
        //스킬강화2 버전으로 검기스킬의 범위를 늘린다.
        swordWave.transform.localScale = new Vector3(2.0f, 1.0f, 2.0f);
    }
    void skillsetting3()
    {
        controller.OnChangeSkills(7);
        //스킬강화3 버전으로 검기에 닿을 시 폭발하여 데미지를 주도록 설계했다.
        //red 버전의 검기 프리팹을 따로 만들어서 스킬강화3를 선택하면 red색상의 검기가 발사된다.
        GameObject swordWave_red = Instantiate(skill_red, transform.position, transform.rotation);
        CharacterSound.instance.OnKnightSkillSound();
        swordWave_red.transform.position = skillPos.position;
        anim.SetTrigger("Attack");
    }
    public void block() // 보호막 함수
    {
        if (die.isPlayerDie == true) // 플레이어 사망 상태일 때 동작에 제한을 두기 위함.
        {
            return;
        }
        //스페이스바를 누르면 보호막 스킬을 사용하여 캐릭터가 방패들 들어 공격을 막을 수 있도록 함수를 구현했다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBlock = true;
            anim.SetBool("block", true);
            shield.Play();
            cap.enabled = false; // 콜라이더를 비활성화하여 데미지를 입지 않게 함.
            StartCoroutine(Endblock()); // 보호막 관련 애니메이션 및 파티클 재생을 위한 코루틴 함수
        }
    }
    IEnumerator Endblock()
    {
        //지속시간 1.5초와 같으며 1.5초가 지나면 다시 idle상태로 되돌아 가고 파티클을 멈추도록 설계했다.
        yield return new WaitForSeconds(1.5f + ItemManager.instance.itemToNonHitTime);
        isBlock = false;
        anim.SetBool("block", false);
        shield.Stop();
        cap.enabled = true;// 보호막 시간이 끝나면 콜라이더가 활성화 되어서 데미지를 입음.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBullet" && isBlock == true)
        {
            //보호막 스킬을 사용하는 동안 캐릭터에 닿는 일반 몬스터들의 탄환들을 삭제함.(데미지X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.MagicBullet);
            CharacterSound.instance.OnKnightShieldSound();
        }
        if (other.gameObject.tag == "GolemBullet" && isBlock == true)
        {
            //보호막 스킬을 사용하는 동안 캐릭터에 닿는 보스 몬스터의 탄환들을 삭제함.(데미지X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.GolemPJ);
            CharacterSound.instance.OnKnightShieldSound();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
