using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGetCoin : MonoBehaviour
{
    Monster monster;
    public int CoinsCount; //ÄÚÀÎ¼ö
   

    private void Start()
    {
        monster = GetComponent<Monster>();
        CoinsCount = monster.monsterData.RewordCoinCount;
    }

    public void Die()
    {
        for (int i = 0; i <CoinsCount; i++)
        {
            GameObject pool = PoolFactroy.instance.GetPool(Consts.Coin);
            pool.transform.position = this.gameObject.transform.position;
            pool.GetComponent<Coin>().StartMovementAndReturnToPool(this.gameObject.transform.position);
            
        }
    }

  
}
