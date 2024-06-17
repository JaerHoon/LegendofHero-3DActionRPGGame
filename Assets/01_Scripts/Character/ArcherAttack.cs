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
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    GameObject arrow; // ȭ�� ������Ʈ
    [SerializeField]
    ParticleSystem ArrowRain; // �ü� ��ų ��ƼŬ
    [SerializeField]
    ParticleSystem magicShield; // �ü� ��ȣ�� ��ƼŬ
    [SerializeField]
    Transform arrowPos; // ȭ�� �߻�Ǵ� ��ġ
    [SerializeField]
    Transform arrowPos2; // ���ȭ��� �߻� ��ġ
    [SerializeField]
    Transform shieldPos; // ��ȣ�� ���� ��ġ

    Animator anim;

    public bool isSkillSetting1 = false; // ��ų����1���� �÷���
    public bool isSkillsSetting2 = false; // ��ų����2���� �÷���
    public bool isButtonPressed3 = false; // ��ų����3���� �÷���

    public bool isAttackButton1 = false; // �⺻����1���� �÷���
    public bool isAttackButton2 = false; // �⺻����2���� �÷���
    public bool isAttackButton3 = false; // �⺻����3���� �÷���
    public bool isShooting = false; // �⺻����3�� �Լ��� ���̴� �÷��׷� �������϶� ������ ��� �̿��ϱ� ����
    public bool isCoolTime = false; // ��ų ��Ÿ�� �÷���
    public bool isBlock = false;
    float lastshotTime; // �⺻����3�� �����Ҷ� ��� ���� �÷���
    float DestroyDuration = 1.5f; // ��ų���� 2���� ���̴� Destroy ���� �÷���
    float DestroyLifeTime = 2.0f; // ��ų���� 2���� ���̴� Destroy ���� �÷���

    int skillCount = 0; // ��ų���� 3���� ���̴� 2�� ��ų�� �����ֱ� ���� ī��Ʈ ����
    float skillcoolTime = 5.0f; // ���Ƿ� ������ ��ų ��Ÿ��
    ArcherTrigger archerTrigger;
    Arrow arrowTrigger;
    CapsuleCollider cap;
    CharacterDamage die;
    Coroutine holdAttack;
    Archer archerController;
    bool isCoolTimeBlock = false;
    ParticleSystem arrowRain;
    void Start()
    {
        anim = GetComponent<Animator>();
        archerTrigger = GetComponent<ArcherTrigger>();
        arrowTrigger = GetComponent<Arrow>();
        cap = GetComponent<CapsuleCollider>();
        die = GetComponent<CharacterDamage>();
        archerController = GameObject.FindWithTag("Player").GetComponent<Archer>();
        
    }

    void shotArrow() // �⺻���� �� �� ȭ�� ���� �� ��ġ�� ������ �Լ�
    {
        GameObject arrowShot = PoolFactroy.instance.GetPool(Consts.Arrow);
        arrowShot.transform.position = arrowPos.position;
        arrowShot.transform.rotation = transform.rotation;
        TrailRenderer Tr = arrowShot.GetComponentInChildren<TrailRenderer>();
        Tr.enabled = true;
    }

    public void ArrowAttack() // �ü� ĳ������ �⺻������ ���� �Լ�
    {
        switch (SkillManager.instance.gainedSkill_Archer[0].id)
        {
            case 0:
                baseAttack(); // �⺻����
                break;
            case 1:
                baseAttack();
                Invoke("AttackSetting1", 0.3f); // �⺻���� ��ȭ 1������
                break;
            case 2:
                AttackSetting2();// �⺻���� ��ȭ 2������
                break;
            case 3:
                AttackSetting3(); // �⺻���� ��ȭ 3������
                break;
        }

    }

    void baseAttack()
    {
        // ���� �ִϸ��̼ǿ� ���缭 ȭ���� ������ �ϱ� ���� 0.3���� �����̸� ��
        Invoke("shotArrow", 0.3f);
        //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
        anim.SetTrigger("Attack");
    }

    public void AttackStop()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (holdAttack != null)
            {
                StopCoroutine(holdAttack);
                holdAttack = null; // �ڷ�ƾ�� ������ �� null�� ����
            }
            isShooting = false;
            anim.SetBool("holdAttack", false);
        }
        
    }

    void AttackSetting1() // �⺻���� 1�� �������� ��� ������� ȭ���� �߻��.
    {
        archerController.OnChangeSkills(1);
        GameObject arrowShot2 = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot2.transform.position = arrowPos2.position;
        
    }

    void AttackSetting2() // �⺻���� 2�� �������� ������ ȭ�� �ѹ��� ���̾ ������.
    {
        archerController.OnChangeSkills(2);
        baseAttack();
        StartCoroutine(repeatingArrow());
    }
    IEnumerator repeatingArrow()
    {
        yield return new WaitForSeconds(0.15f);
        shotArrow();
    }


    void AttackSetting3() // �⺻����3������ ���콺 ������ ������ �ִ� ���� ���� �������� ȭ���� ��� �߻��.
    {
        if(Input.GetMouseButton(0))
        {
            archerController.OnChangeSkills(3);
            isShooting = true;
            anim.SetBool("holdAttack", true);
            
        }
        holdAttack = StartCoroutine(holdArrowAttack());
    }

    IEnumerator holdArrowAttack()
    {
        while (isShooting)
        {
            if (Time.time - lastshotTime >= 0.2f)
            {
                shotArrow();
                lastshotTime = Time.time;
            }
            yield return null;
        }
    
    }

    public void skillAttack() // ��ų�� �ߵ��ϱ� ���� �Լ� => ���콺 ������ ��ư�� ������ ��ų�� �ߵ��ȴ�.
    {
        print(SkillManager.instance.gainedSkill_Archer[1].id);
        switch (SkillManager.instance.gainedSkill_Archer[1].id)
        {
            
            case 4:
                skillBaseAttack(4); // �⺻����
                break;
            case 5:
                skillSetting1(); // ��ų ��ȭ 1������
                break;
            case 6:
                skillSetting2(); // ��ų ��ȭ 2������
                break;
            case 7:
                skillSetting3(); // ��ų ��ȭ 3������
                break;
        }

    }

    void skillBaseAttack(int num)
    {
        //���� �ִϸ��̼ǰ� ��ũ�� ������� ���߱� ���ؼ� 0.4�ʰ��� �����̸� ��
        StartCoroutine(usedray(num));
        //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
        anim.SetTrigger("skillAttack");
    }

    IEnumerator usedray(int num)
    {
        yield return new WaitForSeconds(0.4f);
        usedRay(num);
    }

    void skillSetting1() // ��ų���� 1�� �Լ�
    {
        archerController.OnChangeSkills(5);
        skillBaseAttack(5);
       
    }

    
    void skillSetting2() // ��ų���� 2�� �Լ�
    {
        isSkillsSetting2 = true;
        archerController.OnChangeSkills(6);
        skillBaseAttack(6);
    }

    void skillSetting3() // ��ų���� 3�� �Լ�
    {
        
        archerController.OnChangeSkills(7);
        skillBaseAttack(7);
        /*skillCount++;
        if(skillCount >=2)
        {
            
            StartCoroutine(coolTimeStart());
        }*/
    }

    /*IEnumerator coolTimeStart()
    {
        isCoolTime = true;
        Debug.Log("��Ÿ�� 7�� ����");
        yield return new WaitForSeconds(skillcoolTime);

        isCoolTime = false;
        skillCount = 0;
        Debug.Log("��ų�� ����� �� �ֽ��ϴ�");
    }*/

    void usedRay(int num)
    {
        //Plane�� ���� �������� ����
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        //ScreenPointToRay �Լ��� �̿��ؼ� ���콺 Ŭ���� ��ġ�� 3D ���� ��ǥ������ ��ȯ�Ѵ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float disToPlane;

        if(plane.Raycast(ray, out disToPlane)) // Ray�� plane�� �����ϴ����� �˻���.
        {
            Vector3 hitPoint = ray.GetPoint(disToPlane); // ��ġ ���
                                                         // hitPoint ��ġ�� ���ο� ���� ��ƼŬ ����
            GameObject ps = PoolFactroy.instance.GetPool(Consts.ArrowRain);
            ps.transform.position = hitPoint;
            ps.transform.rotation = transform.rotation;
            arrowRain = ps.GetComponent<ParticleSystem>();
            StartCoroutine(offarrowRain(ps));



            if (num == 5)
            {
                isSkillSetting1 = true;

                ArrowRainParticle arp = arrowRain.GetComponent<ArrowRainParticle>();
                if (arp != null) arp.ParticleColor();
            }
            else if (num == 6)
            {
                if (arrowRain != null)
                {
                    ArrowRainParticle arp = arrowRain.GetComponent<ArrowRainParticle>();
                    if (arp != null) arp.ParticleControl();


                    //���ο췹�� ��ƼŬ���� Emission�� Count���� �����ϴ� �ڵ�� 2��° ��ų������ �� ���� Ÿ����
                    //ǥ���ϱ� ���ؼ� �������� ȭ���� ������ �� ���ƺ��̰� �ϱ� ���� Count���� �����ϴ� �ڵ��̴�.
                    var emi = arrowRain.emission;
                    var bur = new ParticleSystem.Burst[emi.burstCount];
                    emi.GetBursts(bur);

                    for (int i = 0; i < bur.Length; i++)
                    {
                        bur[i].count = 4;
                    }

                    emi.SetBursts(bur);

                }
                
            }
            arrowRain.Play();

            /*if (isButtonPressed2)
            {
                //��ƼŬ�� �����ǰ� ������ �ð��� ���Ŀ� ��ƼŬ�� �� �ִ� ���ӿ�����Ʈ�� Destroy�Ѵ�.
                //���÷� duration =2��, startLifetime= 0.5�ʷ� �����ߴٸ� 2.5�ʵڿ� Destroy�Ѵٴ� ���̴�.
                Invoke("offarrowRain", 2.5f);
                //Destroy(arrowRain.gameObject, DestroyDuration + DestroyLifeTime);
            }*/

        }

        
        /*RaycastHit hit; // ����ĳ��Ʈ �浹 ���� ����

        // ����ĳ��Ʈ �̿� => ��ȯ�� ray���� �̿��ؼ� �浹�� �߻��ϸ� hit�� �浹 ���� ����
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 objPos = hit.point; // ��ƼŬ �����ų ��ġ���� �־��ش�.
            ParticleSystem ps = Instantiate(ArrowRain, objPos, transform.rotation); // Ŭ���� ��ġ�� ������ ��ƼŬ�� �־��ش�.
            ps.Play(); // ��ƼŬ �ý����� �����Ų��.
            
            if(isButtonPressed2)
            {
                Destroy(ps.gameObject, DestroyDuration + DestroyLifeTime);
            }
            else
            {
                //��ƼŬ�� �����ǰ� ������ ��ƼŬ�� �Ҹ�Ǹ� ��ƼŬ�� �� �ִ� ���ӿ�����Ʈ�� Destroy�Ѵ�.
                //���÷� duration =2��, startLifetime= 0.5�ʷ� �����ߴٸ� 2.5�ʵڿ� Destroy�Ѵٴ� ���̴�.
                Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            
        }*/
    }

    IEnumerator offarrowRain(GameObject psOff)
    {
        if(isSkillsSetting2)
        {
            yield return new WaitForSeconds(3.5f);
            PoolFactroy.instance.OutPool(psOff, Consts.ArrowRain);
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
            PoolFactroy.instance.OutPool(psOff, Consts.ArrowRain);
        }
    }

    public void block() // �ü� ��ȣ�� ��ų�� ����ϱ� ���� �Լ�
    {
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCoolTimeBlock = true;
            isBlock = true;
            magicShield.Play();
            magicShield.transform.position = shieldPos.position;
            cap.enabled = false;
            StartCoroutine(Endblock());
            
        }
    }

    IEnumerator Endblock()
    {
        yield return new WaitForSeconds(1.5f + ItemManager.instance.itemToNonHitTime);
        isBlock = false;
        magicShield.Stop();
        cap.enabled = true;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBullet" && isBlock == true)
        {
            //��ȣ�� ��ų�� ����ϴ� ���� ĳ���Ϳ� ��� źȯ���� ������.(������X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.MagicBullet);
        }
        if (other.gameObject.tag == "GolemBullet" && isBlock == true)
        {
            //��ȣ�� ��ų�� ����ϴ� ���� ĳ���Ϳ� ��� źȯ���� ������.(������X)
            PoolFactroy.instance.OutPool(other.gameObject, Consts.GolemPJ);
            print("������Ʈ �����!");
        }
    }

    //**********��ư ������ ���� �Լ�**********//  
    public void OnArcherAttackButton_First()
    {
        SkillManager.instance.gainedSkill_Archer[0] = SkillManager.instance.archerSkills[1];
    }

    public void OnArcherAttackButton_Second()
    {
        SkillManager.instance.gainedSkill_Archer[0] = SkillManager.instance.archerSkills[2];
    }

    public void OnArcherAttackButton_Third()
    {
        SkillManager.instance.gainedSkill_Archer[0] = SkillManager.instance.archerSkills[3];


    }

    public void OnButtonArcherSkill_First()
    {
        SkillManager.instance.gainedSkill_Archer[1] = SkillManager.instance.archerSkills[5];
    }

    public void OnButtonArcherSkill_Second()
    {
        SkillManager.instance.gainedSkill_Archer[1] = SkillManager.instance.archerSkills[6];

    }

    public void OnButtonArcherSkill_Third()
    {
        SkillManager.instance.gainedSkill_Archer[1] = SkillManager.instance.archerSkills[7];
    }

    // Update is called once per frame
    void Update()
    {
        if(die.isPlayerDie == true)
        {
            return;
        }

        AttackStop();
        //ArrowAttack();
        //skillAttack();
        //block();
    }
}
