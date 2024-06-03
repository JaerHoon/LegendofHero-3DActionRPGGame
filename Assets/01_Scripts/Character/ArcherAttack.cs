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
    Transform shieldPos; // ��ȣ�� ���� ��ġ

    Animator anim;
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
        if (Input.GetMouseButtonDown(0)) // ���콺 ���ʹ�ư Ŭ������ �� �ߵ��ϵ��� �����Ѵ�.
        {
            //usedRay();
            Invoke("shotArrow", 0.3f);
            //���콺 Ŭ���� ���� �ִϸ��̼��� �ߵ��ȴ�.
            anim.SetTrigger("Attack");
            //���� ��ǿ� ���缭 ������ ��ƼŬ �ִϸ��̼� ����
            
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

    // Update is called once per frame
    void Update()
    {
        ArrowAttack();
        skillAttack();
        block();

    }
}
