using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    [SerializeField]
    ParticleSystem slash;
    [SerializeField]
    ParticleSystem slasher;

    Animator anim;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void usedRay()
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
    }


    public void KnightAttack()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            usedRay();
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            slash.Play();
        }

    }

    public void skillAttack()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 �����ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            usedRay();
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            slasher.Play();

        }
    }


    // Update is called once per frame
    void Update()
    {
        KnightAttack();
        skillAttack();

    }
}
