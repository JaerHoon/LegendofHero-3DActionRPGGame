using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : UIModel
{
    public Sprite characterIcon;
    public List<GameObject> hearts = new List<GameObject>(new GameObject[5]);
    public int playerHP;
    public int gold_Value;

    Character player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player.ChangeUI += UpDateUI;

        Setting();
    }

    void Setting()
    {
        if(CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            characterIcon = CharacterManager.instance.WarriorIcon;
        }
        else
        {
            characterIcon = CharacterManager.instance.ArcherIcon;
        }
        UpDateUI();
    }

    void UpDateUI()
    {
        playerHP = player.PlayerHp;

        for(int i = 0; i < playerHP; i++)
        {
            hearts[i].SetActive(true);
        }

        for (int i=4; i < 5 - playerHP; i--)
        {
            hearts[i].SetActive(false);
        }

        gold_Value = player.GoldValue;

        ChangeUI();
    }
}
