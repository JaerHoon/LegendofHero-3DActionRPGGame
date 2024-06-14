using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float explosionForce = 10f; // 코인 튀어오르는 힘
    public float upForce = 5f; // 코인이 위로 튀어오르는 힘
    public float coinLifetime = 3f; // 코인이 사라지기 전까지의 시간

    Character player;

    private void Start()
    {
        player = FindFirstObjectByType<Character>();
    }

    private void OnEnable()
    {
       
    }
    public void StartMovementAndReturnToPool(Vector3 pos)
    {
        Vector3 startPosition = pos;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)).normalized;
        Vector3 targetPosition = startPosition + randomDirection * explosionForce;
        float duration = 0.5f; // 코인이 튀어오르는 시간

        StartCoroutine(MoveAndReturn(startPosition, targetPosition, duration));
    }

    private IEnumerator MoveAndReturn(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        // 코인이 튀어오르는 동안 위치 업데이트
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 코인이 땅에 떨어지는 동안 위치 업데이트
        Vector3 fallStartPosition = transform.position;
        Vector3 fallTargetPosition = new Vector3(fallStartPosition.x, 0.5f, fallStartPosition.z);
      
        while (transform.position.y > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, fallTargetPosition, Time.deltaTime*upForce*8);
            yield return null;
        }

        // 일정 시간 후 코인을 풀로 반환
        yield return new WaitForSeconds(1f); // 이동 시간을 반영하지 않음
        player.GoldValue += 10;
        PoolFactroy.instance.OutPool(gameObject, Consts.Coin);
    }

}
