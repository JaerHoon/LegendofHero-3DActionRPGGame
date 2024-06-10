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

    public bool isButtonPressed1 = false; // ��ų����1���� �÷���
    public bool isButtonPressed2 = false; // ��ų����2���� �÷���
    public bool isButtonPressed3 = false; // ��ų����3���� �÷���

    public bool isAttackButton1 = false; // �⺻����1���� �÷���
    public bool isAttackButton2 = false; // �⺻����2���� �÷���
    public bool isAttackButton3 = false; // �⺻����3���� �÷���
    public bool isShooting = false; // �⺻����3�� �Լ��� ���̴� �÷��׷� �������϶� ������ ��� �̿��ϱ� ����
    public bool isCoolTime = false; // ��ų ��Ÿ�� �÷���
    public bool isFreeze = false;
    public bool isBlock = false;
    float lastshotTime; // �⺻����3�� �����Ҷ� ��� ���� �÷���
    float DestroyDuration = 1.5f; // ��ų���� 2���� ���̴� Destroy ���� �÷���
    float DestroyLifeTime = 2.0f; // ��ų���� 2���� ���̴� Destroy ���� �÷���

    int skillCount = 0; // ��ų���� 3���� ���̴� 2�� ��ų�� �����ֱ� ���� ī��Ʈ ����
    float skillcoolTime = 7.0f; // ���Ƿ� ������ ��ų ��Ÿ��
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

    void shotArrow() // �⺻���� �� �� ȭ�� ���� �� ��ġ�� ������ �Լ�
    {
        GameObject arrowShot = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot.transform.position = arrowPos.position;
    }

    public void ArrowAttack() // �ü� ĳ������ �⺻������ ���� �Լ�
    {
        if (Input.GetMouseButtonDown(0) && !isAttackButton3) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            // ���� �ִϸ��̼ǿ� ���缭 ȭ���� ������ �ϱ� ���� 0.3���� �����̸� ��
            Invoke("shotArrow", 0.3f);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����

            if(isAttackButton1) // �⺻���� 1�� ����
            {
                Invoke("AttackSetting1", 0.3f);
            }
            else if(isAttackButton2) // �⺻���� 2�� ����
            {
                Invoke("AttackSetting2", 0.3f);
            }
            
        }
        
        //�⺻���� 3�� ������ ���� if��
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

    void AttackSetting1() // �⺻���� 1�� �������� ��� ������� ȭ���� �߻��.
    {
        GameObject arrowShot2 = Instantiate(arrow, transform.position, transform.rotation);
        arrowShot2.transform.position = arrowPos2.position;
    }

    void AttackSetting2() // �⺻���� 2�� �������� ������ ȭ�� �ѹ��� ���̾ ������.
    {
        StartCoroutine(repeatingArrow());
    }
    IEnumerator repeatingArrow()
    {
        yield return new WaitForSeconds(0.15f);
        shotArrow();
    }


    void AttackSetting3() // �⺻����3������ ���콺 ������ ������ �ִ� ���� ���� �������� ȭ���� ��� �߻��.
    {
       if(Time.time - lastshotTime >= 0.2f)
        {
            shotArrow();
            lastshotTime = Time.time;
        }
    }

    
    public void skillAttack() // ��ų�� �ߵ��ϱ� ���� �Լ� => ���콺 ������ ��ư�� ������ ��ų�� �ߵ��ȴ�.
    {
        if (Input.GetMouseButtonDown(1) && !isCoolTime) // ���콺 �����ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            //���� �ִϸ��̼ǰ� ��ũ�� ������� ���߱� ���ؼ� 0.4�ʰ��� �����̸� ��
            Invoke("usedRay", 0.4f);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("skillAttack");
            
            if(isButtonPressed1) // ��ų���� 1���� ����ϱ� ���� ����
            {
                //skillSetting1();
                isFreeze = true;
                Debug.Log("��������!\n ������ �̵��ӵ��� �����մϴ�!");
                StartCoroutine(offFreeze());
            }
            else if(isButtonPressed2) // ��ų���� 2���� ����ϱ� ���� ����
            {
                skillSetting2();
            }
        }
        //��ų���� 3���� ����ϱ� ���� ����
        if(Input.GetMouseButtonDown(1) && isButtonPressed3 && !isCoolTime)
        {
            skillSetting3();
        }

    }

    void skillSetting1() // ��ų���� 1�� �Լ�
    {
        // 10% Ȯ���� ������� �Ŵ� �Լ��̴�.
        float Debuff = Random.Range(0f, 100f);
        if (Debuff < 10.0f)
        {
            isFreeze = true;
            Debug.Log("��������!\n ������ �̵��ӵ��� �����մϴ�!");
            StartCoroutine(offFreeze());
        }
    }

    IEnumerator offFreeze()
    {
        yield return new WaitForSeconds(10.0f);
        isFreeze = false;
    }

    void skillSetting2() // ��ų���� 2�� �Լ�
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
    void skillSetting3() // ��ų���� 3�� �Լ�
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
        Debug.Log("��Ÿ�� 7�� ����");
        yield return new WaitForSeconds(skillcoolTime);

        isCoolTime = false;
        skillCount = 0;
        Debug.Log("��ų�� ����� �� �ֽ��ϴ�");
    }

    void usedRay()
    {
        //ScreenPointToRay �Լ��� �̿��ؼ� ���콺 Ŭ���� ��ġ�� 3D ���� ��ǥ������ ��ȯ�Ѵ�.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // ����ĳ��Ʈ �浹 ���� ����

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
            
        }
    }


    public void block() // �ü� ��ȣ�� ��ų�� ����ϱ� ���� �Լ�
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

    //**********��ư ������ ���� �Լ�**********//  
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
