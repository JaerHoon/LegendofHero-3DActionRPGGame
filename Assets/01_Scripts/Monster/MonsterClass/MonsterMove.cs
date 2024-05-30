using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    protected Monster monster;
    protected Transform playerTr;

    protected bool IsMove;


    private void Start()
    {
        monster = GetComponent<Monster>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public virtual void OnMove()
    {
        IsMove = true;
    }

    public virtual void OffMove()
    {
        IsMove = false;
    }



    protected virtual void Move()
    {

    }

    private void Update()
    {
        if (IsMove)
        {
            Move();
        }
    }
}
