using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; // 플레이어 이동속도 
    [SerializeField]
    float rotSpeed; // 플레이어 회전속도
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
        if (dir.magnitude >= 0.1f) // 입력 방향의 벡터 크기가 0.1 이상일때 동작 => 불필요한 움직임을 줄여준다.
        {
            //현재 이동하려는 방향으로 y축 회전각도 계산 => Atan2 함수를 이용해서 x,z방향으로 이루어진
            //평면상의 각도를 라디안 값으로 반환하는데 Mathf.Rad2Deg 함수를 이용해서 다시 각도로 변환해준다.
            float playerAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            //SmoothDampAngle 함수를 이용해서 현재 각도에서 목표 각도사이를 부드럽게 회전시켜 준다.
            //()값은 순서대로 현재각도,목표각도,현재각도의 속도 저장(ref),목표각도로 이동하는데 걸리는 시간을 나타낸다.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, playerAngle, ref rotSpeed, 0.1f);
            //위에서 계산한 각도값으로 플레이어의 rotation을 설정한다.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //목표 회전 각도를 기준으로 방향 벡터를 계산한다.
            Vector3 moveDir = Quaternion.Euler(0f, playerAngle, 0f) * Vector3.forward;
            //계산된 방향으로 플레이어를 이동시킨다.                             ↓플레이어 이동을 월드좌표계 기준으로 설정한다.
            transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
            anim.SetBool("isMove", true); // 이동시에 run 애니메이션 실행
                                          //anim.CrossFade("Run", 0.3f);

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -moveRange, moveRange);
            position.z = Mathf.Clamp(position.z, -moveRange, moveRange);

            transform.position = position;

        }
        else
        {
            anim.SetBool("isMove", false); // 멈췄을때 idle 상태로 전환
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
        // 넉백이 활성화된 경우 트랜스레이트로 이동
        transform.Translate(knockbackVelocity * Time.deltaTime);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -moveRange, moveRange);
        position.z = Mathf.Clamp(position.z, -moveRange, moveRange);

        transform.position = position;

        // 넉백 지속 시간 감소
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
