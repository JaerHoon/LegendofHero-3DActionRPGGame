using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; // �÷��̾� �̵��ӵ� 
    [SerializeField]
    float rotSpeed; // �÷��̾� ȸ���ӵ�
    [SerializeField]
    float moveRange;

    Animator anim;
    Camera cam;

    private Vector3 knockbackVelocity;
    private float knockbackTime;
    private bool isKnockbackActive;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Move()
    {
        if(ArcherAttack.instance !=null && ArcherAttack.instance.isShooting==true)
        {
            return;
        }
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(moveX, 0f, moveY).normalized;
        if (dir.magnitude >= 0.1f) // �Է� ������ ���� ũ�Ⱑ 0.1 �̻��϶� ���� => ���ʿ��� �������� �ٿ��ش�.
        {
            //���� �̵��Ϸ��� �������� y�� ȸ������ ��� => Atan2 �Լ��� �̿��ؼ� x,z�������� �̷����
            //������ ������ ���� ������ ��ȯ�ϴµ� Mathf.Rad2Deg �Լ��� �̿��ؼ� �ٽ� ������ ��ȯ���ش�.
            float playerAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            //SmoothDampAngle �Լ��� �̿��ؼ� ���� �������� ��ǥ �������̸� �ε巴�� ȸ������ �ش�.
            //()���� ������� ���簢��,��ǥ����,���簢���� �ӵ� ����(ref),��ǥ������ �̵��ϴµ� �ɸ��� �ð��� ��Ÿ����.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, playerAngle, ref rotSpeed, 0.1f);
            //������ ����� ���������� �÷��̾��� rotation�� �����Ѵ�.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //��ǥ ȸ�� ������ �������� ���� ���͸� ����Ѵ�.
            Vector3 moveDir = Quaternion.Euler(0f, playerAngle, 0f) * Vector3.forward;
            //���� �������� �÷��̾ �̵���Ų��.                             ���÷��̾� �̵��� ������ǥ�� �������� �����Ѵ�.
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
            anim.SetBool("isMove", true); // �̵��ÿ� run �ִϸ��̼� ����
                                          //anim.CrossFade("Run", 0.3f);

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -moveRange, moveRange);
            position.z = Mathf.Clamp(position.z, -moveRange, moveRange);

            transform.position = position;

        }
        else
        {
            anim.SetBool("isMove", false); // �������� idle ���·� ��ȯ
            //anim.CrossFade("Idle", 0.3f);
        }

       
    }

    
    private void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {

            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }
    }
    // Update is called once per frame

    public void ApplyKnockback(Vector3 direction, float force, float duration)
    {
        knockbackVelocity = direction * force;
        knockbackTime = duration;
        isKnockbackActive = true;
    }

    void KnockBack()
    {
        // �˹��� Ȱ��ȭ�� ��� Ʈ��������Ʈ�� �̵�
        transform.Translate(knockbackVelocity * Time.deltaTime);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -moveRange, moveRange);
        position.z = Mathf.Clamp(position.z, -moveRange, moveRange);

        transform.position = position;

        // �˹� ���� �ð� ����
        knockbackTime -= Time.deltaTime;
        if (knockbackTime <= 0)
        {
            isKnockbackActive = false;
            knockbackVelocity = Vector3.zero;
        }
    }
    void Update()
    {
        if (!isKnockbackActive)
        {
            Move();
            Rotate();
        }
        else
        {
            KnockBack();
        }
        


    }
}
