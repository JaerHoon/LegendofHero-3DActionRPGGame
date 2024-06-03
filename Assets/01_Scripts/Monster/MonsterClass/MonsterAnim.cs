using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    protected Monster monster;
    protected Animator animator;
    [SerializeField]
    GameObject GenFX;

    protected int Idle = 0;
    protected int Run = 2;
    protected int Attack = 1;
    protected string Hit = "Hit";
    protected string Die = "Die";

    protected string Stat = "Stat";

 
    protected void Init()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
    }

    public virtual void OnRiseAnim()
    {
        
    }

    public virtual void OnGenerateAnim()
    {
        GenFX.SetActive(true);
        StartCoroutine(OffGenerateAnim());
    }

    IEnumerator OffGenerateAnim()
    {
        yield return new WaitForSeconds(2f);
        monster.CheckStart();
        GenFX.SetActive(false);
    }
    public virtual void OnIdleAnim()
    {
        animator.SetInteger(Stat, Idle);
    }

    public virtual void OnAtkAnim()
    {
       
        animator.SetInteger(Stat, Attack);
    }

    public virtual void OffAtkAnim()
    {
      
    }

    public virtual void OnDamageAnim()
    {
        animator.SetTrigger(Hit);
    }

    public virtual void OffDamageAnim()
    {
        monster.OffDamage();
    }

    public virtual void OnMovingAnim()
    {
        animator.SetInteger(Stat, Run);
    }

    public virtual void OffMovingAnim()
    {

    }

    public virtual void OnDyingAnim()
    {
        animator.SetTrigger(Die);
    }

    public virtual void OffDyingAnim()
    {

    }

    public virtual void OnAtk1Anim()
    {

    }

    public virtual void OnAtk2Anim()
    {

    }

    public virtual void OnAtk3Anim()
    {

    }

}
