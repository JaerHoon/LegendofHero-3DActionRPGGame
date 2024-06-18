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
                playerDie?.Invoke();
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

    public delegate void UISetting();
    public UISetting ChangeUI;

    PlayerMoving playerMoving;
    CharacterAttackController characterAttack;

    private void Awake()
    {
        playerMoving = GetComponent<PlayerMoving>();
        characterAttack = GetComponent<CharacterAttackController>();
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
}
