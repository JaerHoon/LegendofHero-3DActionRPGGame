using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public enum ChoicedCharacter { Warrior, Archer}
    public ChoicedCharacter choicedCharacter;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
