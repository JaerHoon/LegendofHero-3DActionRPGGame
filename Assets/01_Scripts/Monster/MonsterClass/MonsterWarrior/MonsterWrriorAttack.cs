using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWrriorAttack : MonsterAttack
{
    [SerializeField]
    float KnockBackSpeed;
    [SerializeField]
    float knockbackForce = 10f; // 넉백 힘의 크기
    [SerializeField]
    float knockbackDuration = 0.2f; // 넉백 지속 시간

    private void Start()
    {
        Init();
        hitBox.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        hitBox.gameObject.SetActive(true);
        Invoke("EndAttack", 0.4f);
        
    }

    public override void EndAttack()
    {
        hitBox.gameObject.SetActive(false);
    }

    public override void Hit(CharacterDamage player)
    {
       
        player.OnDamage(monster.monsterData.ATKPow);
        Vector3 knockbackDirection = monster.playerTr.transform.position - transform.position;
        knockbackDirection.y = 0; // Y축 방향으로는 넉백하지 않도록 설정 (필요에 따라 조정 가능)
        knockbackDirection.Normalize();
        PlayerMoving playerMoving = player.gameObject.GetComponent<PlayerMoving>();
        // 플레이어에게 넉백 효과 적용
        playerMoving.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
    }

}
