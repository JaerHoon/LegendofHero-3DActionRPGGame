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
    public bool isButtonPressed3 = false;
 
    bool isCoolTimeBlock = false;
    bool isAttackButton1 = false;
    bool isAttackButton2 = false;
    public bool isBlock = false;
    public bool isAttackButton3 = false;

    CharacterDamage die;
    CapsuleCollider cap;
    Warrior controller;
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
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
        die = GetComponent<CharacterDamage>();
        cap = GetComponent<CapsuleCollider>();
        controller = GameObject.FindWithTag("Player").GetComponent<Warrior>();
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
        /*if (isBlock == true)
        {
            return;
        }*/

        if (Input.GetMouseButtonDown(0) && !isAttackButton3) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            playerTrigger.OnCollider();
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            slash.Play();
            //���� ������ ��ġ�� �ٲ�� ��찡 �־ ������ ��ġ���� �־��༭ �������� �ش�.
            slash.transform.localPosition = new Vector3(-0.05f,1.44f,0.87f);
            

            if (isAttackButton1)
            {
                AttackSetting1(); // ���ù�ư1 ������ �� ��Ÿ��ȭ1 �Լ��� �ߵ��Ѵ�.
            }
            else if(isAttackButton2)
            {
                AttackSetting2(); // ���ù�ư2 ������ �� ��Ÿ��ȭ2 �Լ��� �ߵ��Ѵ�.
            }
            

        }
        if (Input.GetMouseButtonDown(0) && isAttackButton3)
        {
            //���ù�ư3 ������ �� ��Ÿ��ȭ3 �Լ��� �ߵ��Ѵ�.
            //��Ÿ��ȭ3�� ������ else if�� ���� �ɾ��� �� ������ ���� bool�� ������ �ɾ��ָ鼭 if���� ���� ������.
            AttackSetting3();
        }


    }

    void AttackSetting1()
    {
        controller.OnChangeSkills(1);
        //��Ÿ��ȭ1 ���� => ������ �÷��ش�.
        slash.transform.localScale = new Vector3(1.7f, 1.0f, 1.7f);
        
    }

    void AttackSetting2()
    {
        controller.OnChangeSkills(2);
        var mainColor = slash.main;
        mainColor.startColor = Color.magenta; // ����or�� �̹����� �����ؼ� ��Ÿ��ȭ2�� ������ �������� ��������� ���Ѵ�.
        
        // 10% Ȯ���� ������� �Ŵ� �Լ��̴�.
        float Debuff = Random.Range(0f, 100f);
        if (Debuff < 10.0f)
        {
            Debug.Log("���ָ� �ɾ����ϴ�!");
        }
    }

    void AttackSetting3()
    {
        controller.OnChangeSkills(3);
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
        if(isBlock==true)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(1) && !isButtonPressed3) // ���콺 �����ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            anim.SetTrigger("Attack");
            //�˱� ��ų �߻��ϵ��� Instantiate �̿��Ѵ�.
            GameObject swordWave = Instantiate(skill, transform.position, transform.rotation);
            //3��° ��ų�� ���߰˱⸦ �̿��� �� ���� ��ƼŬ�� �����Ű�� ���� ������ ����ش�.
            //�̷��� ���ϸ� clone�� ������ �˱�鿡 ��ƼŬ�� None ���·� ���� ��ƼŬ ����� �ȵǱ� �����̴�.
            swordWave.GetComponent<SwordWave>().Ex = waveEX;
            // ĳ���� �� �ʿ� ��ġ���Ѽ� �̻��� ������ �ȳ������� ������Ű�� �����̴�.
            swordWave.transform.position = skillPos.position;
            
            if (isButtonPressed)
            {
                skillsetting1(); // ��ų1 ��ư ������ ��ų��ȭ1 �Լ� �ߵ��Ѵ�.
            }
            else if (isButtonPressed2)
            {
                skillsetting2(swordWave); // ��ų2 ��ư ������ ��ų��ȭ2 �Լ� �ߵ��Ѵ�.
            }
            
        }

        if(Input.GetMouseButtonDown(1) && isButtonPressed3)
        {
            anim.SetTrigger("Attack");
            skillsetting3();
        }
    }
    
    //**********��ư ������ ���� �Լ�**********//  
    public void OnAttackButton_First()
    {
        isAttackButton1 = !isAttackButton1;
    }

    public void OnAttackButton_Second()
    {
        isAttackButton2 = !isAttackButton2;
    }

    public void OnAttackButton_Third()
    {
        isAttackButton3 = !isAttackButton3;
    }

    public void OnButtonSkill_First()
    {
        isButtonPressed = !isButtonPressed;
    }

    public void OnButtonSkill_Second()
    {
        isButtonPressed2 = !isButtonPressed2;
        
    }

    public void OnButtonSkill_Third()
    {
        isButtonPressed3 = !isButtonPressed3;
    }

    void skillsetting1()
    {

        controller.OnChangeSkills(5);
        //��ų��ȭ1 �������� �ء�� �������� ���ư��� ���� �آ� �������� ������ �˱�鸸 ���� ������ �־���.
        Quaternion WaveRot2 = transform.rotation * Quaternion.Euler(0, 50.0f, 0);
        Quaternion WaveRot3 = transform.rotation * Quaternion.Euler(0, -50.0f, 0);

        GameObject swordWave2 = Instantiate(skill, transform.position, WaveRot2);
        GameObject swordWave3 = Instantiate(skill, transform.position, WaveRot3);
        swordWave2.transform.position = skillPos2.position;
        swordWave3.transform.position = skillPos3.position;
    }

    void skillsetting2(GameObject wave)
    {
        controller.OnChangeSkills(6);
        //��ų��ȭ2 �������� �˱⽺ų�� ������ �ø���.
        wave.transform.localScale = new Vector3(2.0f, 1.0f, 2.0f);
    }

    void skillsetting3()
    {
        controller.OnChangeSkills(7);
        //��ų��ȭ3 �������� �˱⿡ ���� �� �����Ͽ� �������� �ֵ��� �����ߴ�.
        //red ������ �˱� �������� ���� ���� ��ų��ȭ3�� �����ϸ� red������ �˱Ⱑ �߻�ȴ�.
        GameObject swordWave_red = Instantiate(skill_red, transform.position, transform.rotation);
        swordWave_red.GetComponent<SwordWave>().Ex = waveEX;
        swordWave_red.transform.position = skillPos.position;
    }
    

    public void block() // ��ȣ�� �Լ�
    {
        
        //�����̽��ٸ� ������ ��ȣ�� ��ų�� ����Ͽ� ĳ���Ͱ� ���е� ��� ������ ���� �� �ֵ��� �Լ��� �����ߴ�.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isCoolTimeBlock = true;
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
        yield return new WaitForSeconds(1.5f);
        isBlock = false;
        anim.SetBool("block", false);
        shield.Stop();
        cap.enabled = true;// ��ȣ�� �ð��� ������ �ݶ��̴��� Ȱ��ȭ �Ǿ �������� ����.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MagicBullet" && isBlock == true)
        {
            //��ȣ�� ��ų�� ����ϴ� ���� ĳ���Ϳ� ��� źȯ���� ������.(������X)
            Destroy(other.gameObject);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if(die.isPlayerDie==true) // �÷��̾� ��� ������ �� ���ۿ� ������ �α� ����.
        {
            return;
        }

        //KnightAttack();
        //skillAttack();
        //block();
        
    }
}
