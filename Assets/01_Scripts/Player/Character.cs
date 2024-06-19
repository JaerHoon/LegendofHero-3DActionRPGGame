using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    protected int playerHp = 5;
    public Action playerDie;
    public int PlayerHp
    {
        get { return playerHp; }
        set
        {
            if (value <= 0)
            {
                playerHp = 0;
                ChangeUI?.Invoke();
                Die();
            }
            else
            {
                playerHp = value;
               
                ChangeUI?.Invoke();
            }

        }
    }

    protected int goldValue=0;
    public int GoldValue
    {
        get { return goldValue; }
        set 
        {
            goldValue = value;
           
            ChangeUI?.Invoke();
            
        }
    }

    CapsuleCollider capsuleCollider;
    Animator anim;

    public delegate void UISetting();
    public UISetting ChangeUI;

    PlayerMoving playerMoving;
    CharacterAttackController characterAttack;
    InGameCanvasController inGameCanvs;

    private void Awake()
    {
        inGameCanvs = FindFirstObjectByType<InGameCanvasController>();
        playerMoving = GetComponent<PlayerMoving>();
        characterAttack = GetComponent<CharacterAttackController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        capsuleCollider.enabled = true;
    }

    public void PlayerReset()
    {
        playerHp = 5;
        goldValue = 0;
        SkillManager.instance.ReSet();
        ChangeUI?.Invoke();
        characterAttack.PlayerReset();
    }

    public void OnPlay()
    {
        playerMoving.OnMove();
        characterAttack.CanATK();
    }

    public void OffPlay()
    {
        playerMoving.OffMove();
        characterAttack.DontATK();
    }

    void Die()
    {
        playerDie?.Invoke();
        OffPlay();
        anim.SetTrigger("Death");
        inGameCanvs.OnGameOverPanel();

        Invoke("AfterDie", 0.5f);
        
    }

    void AfterDie()
    {
       
    }
}
