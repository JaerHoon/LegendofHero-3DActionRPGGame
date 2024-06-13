using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWrriorMove : MonsterMove
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
