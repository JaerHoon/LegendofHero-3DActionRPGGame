using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMageMove : MonsterMove
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
