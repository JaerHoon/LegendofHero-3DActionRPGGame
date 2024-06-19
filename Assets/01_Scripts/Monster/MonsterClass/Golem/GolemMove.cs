using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMove : MonsterMove
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void Move()
    {
        if(monster.playerTr == null)
        {
            agent.destination = this.transform.position;
            IsMove = false;
        }
        else
        {
            
            agent.destination = monster.playerTr.position;
        }
    }

  
}
