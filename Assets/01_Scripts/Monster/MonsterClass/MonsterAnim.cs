using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    protected Monster monster;
    protected Animator animator;

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
 
    public virtual void OnIdleAnim()
    {
        animator.SetInteger(Stat, Idle);
    }

    public virtual void OnAtkAnim()
    {
        print("ATK");
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


   
    
}
