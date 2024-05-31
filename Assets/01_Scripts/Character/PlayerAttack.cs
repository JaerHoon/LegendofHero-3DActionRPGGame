using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    ParticleSystem shield;
    [SerializeField]
    GameObject skill;
    [SerializeField]
    Transform skillPos;
    [SerializeField]
    float skillPower;

    Animator anim;
    Rigidbody rb;
    PlayerTrigger playerTrigger;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
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
        if (Input.GetMouseButtonDown(0)) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            playerTrigger.OnCollider();
            //usedRay();
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            slash.Play();
            slash.transform.localPosition = new Vector3(-0.05f,1.44f,0.87f);
        }

    }

    public void skillAttack() // ��ų �Լ�
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 �����ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            //usedRay();
            GameObject swordWave = Instantiate(skill, transform.position, transform.rotation);
            swordWave.transform.position = skillPos.position;
            GameObject swordWave2 = Instantiate(skill, transform.position,transform.rotation);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            //slasher.Play();

        }
    }

    public void block() // ��ȣ�� �Լ�
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("block", true);
            shield.Play();
            StartCoroutine(Endblock());
        }
    }

    IEnumerator Endblock()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("block", false);
        shield.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        KnightAttack();
        skillAttack();
        block();

    }
}
