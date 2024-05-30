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
    GameObject arrow;
    //[SerializeField]
    //ParticleSystem arrowRain;
    [SerializeField]
    ParticleSystem magicShield;
    [SerializeField]
    Transform arrowPos;
    [SerializeField]
    Transform shieldPos;

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
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            //usedRay();
            Invoke("shotArrow", 0.3f);
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            
        }

    }

    public void skillAttack()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽버튼 클릭했을 때 발동하도록 설정한다.
        {
            //usedRay();
            //마우스 클릭시 공격 애니메이션이 발동된다.
            anim.SetTrigger("Attack");
            //공격 모션에 맞춰서 슬래시 파티클 애니메이션 실행
            //slasher.Play();

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
