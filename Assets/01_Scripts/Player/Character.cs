using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int playerHp = 5;
    public int PlayerHp
    {
        get { return playerHp; }
        set
        {
            if (value <= 0)
            {
                playerHp = 0;
                ChangeUI?.Invoke();
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
            value = goldValue;
            ChangeUI?.Invoke();
            
        }
    }

    public delegate void UISetting();
    public UISetting ChangeUI;    
}
