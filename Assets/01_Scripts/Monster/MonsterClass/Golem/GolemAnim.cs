using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnim : MonsterAnim
{
    const int WAIT = 0;
    const int RISE = 1;
    const int IDLE = 2;
    const int WALK = 3;
    const int ATTACK = 4;
    const int ATTACKP = 5;
    const int ATTACKG = 6;

    [SerializeField]
    ParticleSystem GroundAttackEffect;
    [SerializeField]
    ShakeCamera shakeCameraSC;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void OnWaitAnim()
    {
        animator.SetInteger(Stat, WAIT);
    }

    public override void OnRiseAnim()
    {
        animator.SetInteger(Stat, RISE);
    }

    public override void OnIdleAnim()
    {
        animator.SetInteger(Stat, IDLE);
    }

    public override void OnAtk1Anim()//
    {
        animator.SetInteger(Stat, ATTACK);
        Invoke("DelayIdle", 0.3f);
    }

    public override void OnAtk2Anim()
    {
        animator.SetInteger(Stat, ATTACKP);
        Invoke("DelayIdle", 0.3f);
    }

    public override void OnAtk3Anim()
    {
        animator.SetInteger(Stat, ATTACKG);
        GroundAttackEffect.Play();
        Invoke("DelayShakeCamera", 0.8f);
        Invoke("DelayIdle", 0.3f);

    }

    void DelayShakeCamera()
    {
        shakeCameraSC.StartShake(1f, 2, 2);
    }


    public override void OnMovingAnim()
    {
        animator.SetInteger(Stat, WALK);
    }

    public override void OnDyingAnim()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        animator.SetTrigger(Die);
        Invoke("DestroyGameObject", 3.0f);
    }

    void DestroyGameObject()
    {
        PoolFactroy.instance.OutPool(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    void DelayIdle()
    {
        animator.SetInteger(Stat, IDLE);
    }

    public override void OnGenerateAnim()
    {
        
    }
}
