using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnim : MonsterAnim
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public override void OnIdleAnim()
    {
        animator.CrossFade("Anim_Idle", 0.5f);
    }

    public override void OnAtkAnim()
    {
        animator.CrossFade("Anim_Attack1", 0.5f);
    }
    public override void OnMovingAnim()
    {
        animator.CrossFade("Walk", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
