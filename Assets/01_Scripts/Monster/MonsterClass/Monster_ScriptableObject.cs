using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string MonsterName;
    public string MonsterID;
    public float HP;
    public float ATKPow;
    public int Lv;
    public float MoveSpeed;
    public float ATKSpeed;

}
  
