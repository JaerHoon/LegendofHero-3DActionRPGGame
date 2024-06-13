using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMinionMove : MonsterMove
{
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        ReSet();
    }
}
