using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    protected Monster monster;
    protected Transform playerTr;

    private void Start()
    {
        monster = GetComponent<Monster>();
        playerTr = GameObject.FindGameObjectWithTag("Palyer").transform;
    }

    public virtual void OnIdleAnim()
    {

    }

    public virtual void OnAtkAnim()
    {

    }

    public virtual void OffAtkAnim()
    {
      
    }

    public virtual void OnDamageAnim()
    {

    }

    public virtual void OffDamageAnim()
    {
        monster.OffDamage();
    }

    public virtual void OnMovingAnim()
    {

    }

    public virtual void OffMovingAnim()
    {

    }

    public virtual void OnDyingAnim()
    {

    }

    public virtual void OffDyingAnim()
    {

    }
}
