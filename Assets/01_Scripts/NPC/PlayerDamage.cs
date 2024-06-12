using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : CharacterDamage
{
    Character player;

    private void Awake()
    {
        player = GetComponent<Character>();
    }
    public override void OnDamage(float dmg)
    {
        if (PlayerAttack.instance != null && PlayerAttack.instance.isBlock == true ||
          ArcherAttack.instance != null && ArcherAttack.instance.isBlock == true)
        {
            return; // ��ȣ�� ��ų ������� �� �������� ���� �ʰ� �ϱ� ����.
        }
        //print("�÷��̾� �ǰ� ������ : " + dmg);
        cap.enabled = false; // �ǰ� �� �ݶ��̴��� �� 2������ ��Ȱ��ȭ �Ͽ� �ǰ� �����ð��� ����
        box.enabled = false; // �ǰ� �� �ݶ��̴��� �� 2������ ��Ȱ��ȭ �Ͽ� �ǰ� �����ð��� ����
        heart.SetActive(true); // ��Ʈ ��ƼŬ �����Ű�� �ڵ�
        onOffRendererCoroutine = StartCoroutine(onOffRenderer()); // �ǰ� �� ĳ���� ����Ǵ� �ڷ�ƾ
        Invoke("offSetActive", 2.0f); // 2�� �Ŀ� �ݶ��̴� Ȱ��ȭ
        player.PlayerHp--;
      
        heart.SetActive(true);
    }
}
