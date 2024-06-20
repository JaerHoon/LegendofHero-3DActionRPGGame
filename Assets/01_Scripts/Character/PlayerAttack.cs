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
        //ScreenPointToRay �Լ��� �̿��ؼ� ���콺 Ŭ���� ��ġ�� 3D ���� ��ǥ������ ��ȯ�Ѵ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // ����ĳ��Ʈ �浹 ���� ����
        // ����ĳ��Ʈ �̿� => ��ȯ�� ray���� �̿��ؼ� �浹�� �߻��ϸ� hit�� �浹 ���� ����
        if (Physics.Raycast(ray, out hit))
        {
            //�÷��̾ �ٶ󺸴� ���Ⱚ�� �����Ѵ�. transform.position.y���� �÷��̾ ���󿡼� ȸ���ϰ� �����.
            Vector3 lookAtPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //LookAt �Լ��� �̿��Ͽ� lookAtPos ���Ⱚ���� �÷��̾ �ٶ󺸰� �����.
            transform.LookAt(lookAtPos);
        }
    }*/
    public void KnightAttack() // �⺻���� �Լ�
    {
        if (die.isPlayerDie == true) // �÷��̾� ��� ������ �� ���ۿ� ������ �α� ����.
        {
            return;
        }

        switch (SkillManager.instance.gainedSkill_Warrior[0].id)
        {
            case 0:
                baseAttack(); // �⺻����
                break;
            case 1:
                AttackSetting1(); // �⺻���� ��ȭ 1������
                break;
            case 2:
                AttackSetting2(); // �⺻���� ��ȭ 2������
                break;
            case 3:
                AttackSetting3(); // �⺻���� ��ȭ 3������
                break;
        }
    }
    void baseAttack()
    {
        
        playerTrigger.OnCollider();
        //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
        anim.SetTrigger("Attack");
        //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
        slash.Play();
        CharacterSound.instance.OnKnightBaseAttackSound();
        //���� ������ ��ġ�� �ٲ�� ��찡 �־ ������ ��ġ���� �־��༭ �������� �ش�.
        slash.transform.localPosition = new Vector3(-0.05f, 1.44f, 0.87f);
    }
    public void AttackSetting1()
    {
        controller.OnChangeSkills(1);
        //��Ÿ��ȭ1 ���� => ������ �÷��ش�.
        slash.transform.localScale = new Vector3(1.7f, 1.0f, 1.7f);
        baseAttack();
    }
    void AttackSetting2()
    {
        controller.OnChangeSkills(2);
        isAttackSetting2 = true;
        baseAttack();
        var mainColor = slash.main;
        mainColor.startColor = Color.magenta; // ����or�� �̹����� �����ؼ� ��Ÿ��ȭ2�� ������ �������� ��������� ���Ѵ�.
        
    }
    void AttackSetting3()
    {
        controller.OnChangeSkills(3);
        //baseAttack();
        //��Ÿ��ȭ3 �������� Ⱦ�����Ŀ� ������� ������� ���Ӱ����� �Ѵ�.
        playerTrigger.OnCollider();
        anim.SetBool("isAttack",true);
        slash.Play(); // Ⱦ���� ���� �� ������ ������ ��ƼŬ ���
        StartCoroutine(chopslashPlay());
    }
    IEnumerator chopslashPlay()
    {
        yield return new WaitForSeconds(0.4f);
        // �ڷ�ƾ ���ؼ� 0.4�ʵڿ� �������� �� ������ ��ƼŬ�� ����Ѵ�.
        // �������� �� ������ ������ Ÿ�̹��� ���߱� ���ؼ� �ڷ�ƾ�� �̿��Ѵ�.
        playerTrigger.OnCollider();
        chopSlash.Play();
        //������� �������� ������ ��ġ���� �־��༭ ������ ��ġ���� ������� �ʰ� �������� �ش�.
        chopSlash.transform.localPosition = new Vector3(0.9f, 2.4f, 1.5f);
    }
    public void OnAttack2End() // ���Ӱ��� ���Ŀ� idle�� ���ư��� �����̸� �ִϸ��̼� �̺�Ʈ�� ���۵ȴ�.
    {
        anim.SetBool("isAttack", false);
    }
    public void skillAttack() // ��ų �Լ�
    {
        if (die.isPlayerDie == true) // �÷��̾� ��� ������ �� ���ۿ� ������ �α� ����.
        {
            return;
        }
        switch (SkillManager.instance.gainedSkill_Warrior[1].id)
        {
            case 4:
                skillBaseAttack(); // �⺻����
                break;
            case 5:
                skillsetting1(); // ��ų ��ȭ 1������
                break;
            case 6:
                skillsetting2(); // ��ų ��ȭ 2������
                break;
            case 7:
                skillsetting3(); // ��ų ��ȭ 3������
                break;
        }
    }
    void skillBaseAttack()
    {
        anim.SetTrigger("Attack");
        //�˱� ��ų �߻��ϵ��� Instantiate �̿��Ѵ�.
        swordWave = Instantiate(skill, transform.position, transform.rotation);
        CharacterSound.instance.OnKnightSkillSound();
        // ĳ���� �� �ʿ� ��ġ���Ѽ� �̻��� ������ �ȳ������� ������Ű�� �����̴�.
        swordWave.transform.position = skillPos.position;
    }
    void skillsetting1()
    {
        controller.OnChangeSkills(5);
        skillBaseAttack();
        //��ų��ȭ1 �������� �ء�� �������� ���ư��� ���� �آ� �������� ������ �˱�鸸 ���� ������ �־���.
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
        //��ų��ȭ2 �������� �˱⽺ų�� ������ �ø���.
        swordWave.transform.localScale = new Vector3(2.0f, 1.0f, 2.0f);
    }
    void skillsetting3()
    {
        controller.OnChangeSkills(7);
        //��ų��ȭ3 �������� �˱⿡ ���� �� �����Ͽ� �������� �ֵ��� �����ߴ�.
        //red ������ �˱� �������� ���� ���� ��ų��ȭ3�� �����ϸ� red������ �˱Ⱑ �߻�ȴ�.
        GameObject swordWave_red = Instantiate(skill_red, transform.position, transform.rotation);
        CharacterSound.instance.OnKnightSkillSound();
        swordWave_red.transform.position = skillPos.position;
        anim.SetTrigger("Attack");
    }
    public void block() // ��ȣ�� �Լ�
    {
        if (die.isPlayerDie == true) // �÷��̾� ��� ������ �� ���ۿ� ������ �α� ����.
        {
            return;
        }
        //�����̽��ٸ� ������ ��ȣ�� ��ų�� ����Ͽ� ĳ���Ͱ� ���е� ��� ������ ���� �� �ֵ��� �Լ��� �����ߴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBlock = true;
            anim.SetBool("block", true);
            shield.Play();
            cap.enabled = false; // �ݶ��̴��� ��Ȱ��ȭ�Ͽ� �������� ���� �ʰ� ��.
            StartCoroutine(Endblock()); // ��ȣ�� ���� �ִϸ��̼� �� ��ƼŬ ����� ���� �ڷ�ƾ �Լ�
        }
    }
    IEnumerator Endblock()
    {
        //���ӽð� 1.5�ʿ� ������ 1.5�ʰ� ������ �ٽ� idle���·� �ǵ��� ���� ��ƼŬ�� ���ߵ��� �����ߴ�.
        yield return new WaitForSeconds(1.5f + ItemManager.instance.itemToNonHitTime);
        isBlock = false;
        anim.SetBool("block", false);
        shield.Stop();
        cap.enabled = true;// ��ȣ�� �ð��� ������ �ݶ��̴��� Ȱ��ȭ �Ǿ �������� ����.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBullet" && isBlock == true)
        {
            //��ȣ�� ��ų�� ����ϴ� ���� ĳ���Ϳ� ��� �Ϲ� ���͵��� źȯ���� ������.(������X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.MagicBullet);
            CharacterSound.instance.OnKnightShieldSound();
        }
        if (other.gameObject.tag == "GolemBullet" && isBlock == true)
        {
            //��ȣ�� ��ų�� ����ϴ� ���� ĳ���Ϳ� ��� ���� ������ źȯ���� ������.(������X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.GolemPJ);
            CharacterSound.instance.OnKnightShieldSound();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
