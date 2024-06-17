using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
   
    public SkillData KnightData;
   
    public SkillData ArcherData;
    
    public enum ChoicedCharacter { Warrior, Archer}
    public ChoicedCharacter choicedCharacter;
    public Sprite WarriorIcon;
    public Sprite ArcherIcon;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
