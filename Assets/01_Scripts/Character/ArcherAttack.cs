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
        if (Input.GetMouseButtonDown(0) && !isAttackButton3) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            Invoke("shotArrow", 0.3f);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����

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
        if (Input.GetMouseButtonDown(1)) // ���콺 �����ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            Invoke("usedRay", 0.4f);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("skillAttack");
            

        }
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
            //��ƼŬ�� �����ǰ� ������ ��ƼŬ�� �Ҹ�Ǹ� ��ƼŬ�� �� �ִ� ���ӿ�����Ʈ�� Destroy�Ѵ�.
            //���÷� duration =2��, startLifetime= 0.5�ʷ� ���������Ƿ� 2.5�ʵڿ� Destroy�Ѵ�.
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
