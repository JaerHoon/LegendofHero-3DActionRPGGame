using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMagicBullet : MonoBehaviour
{
    Transform playerTr;
    Vector3 pos;
    [SerializeField]
    float Movespeed;

    public void Setting(Transform playertr)
    {
        playerTr = playertr;

        Vector3 pos = new Vector3(0, 0, 0) - transform.position;
        pos.Normalize();
        transform.position = transform.position + pos;
             
    }

    private void LateUpdate()
    {
        transform.position = 
            Vector3.MoveTowards(gameObject.transform.position, pos, Movespeed);

    }
}

