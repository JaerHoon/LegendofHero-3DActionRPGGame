using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWrriorAttack : MonsterAttack
{
    [SerializeField]
    float KnockBackSpeed;
    [SerializeField]
    float knockbackForce = 10f; // �˹� ���� ũ��
    [SerializeField]
    float knockbackDuration = 0.2f; // �˹� ���� �ð�

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
        knockbackDirection.y = 0; // Y�� �������δ� �˹����� �ʵ��� ���� (�ʿ信 ���� ���� ����)
        knockbackDirection.Normalize();
        PlayerMoving playerMoving = player.gameObject.GetComponent<PlayerMoving>();
        // �÷��̾�� �˹� ȿ�� ����
        playerMoving.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
    }

}
