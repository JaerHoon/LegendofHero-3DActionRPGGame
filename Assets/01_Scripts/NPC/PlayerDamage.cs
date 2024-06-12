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
            return; // 보호막 스킬 사용중일 때 데미지를 입지 않게 하기 위함.
        }
        //print("플레이어 피격 데미지 : " + dmg);
        cap.enabled = false; // 피격 시 콜라이더를 약 2초정도 비활성화 하여 피격 무적시간을 구현
        box.enabled = false; // 피격 시 콜라이더를 약 2초정도 비활성화 하여 피격 무적시간을 구현
        heart.SetActive(true); // 하트 파티클 재생시키는 코드
        onOffRendererCoroutine = StartCoroutine(onOffRenderer()); // 피격 시 캐릭터 점멸되는 코루틴
        Invoke("offSetActive", 2.0f); // 2초 후에 콜라이더 활성화
        player.PlayerHp--;
      
        heart.SetActive(true);
    }
}
