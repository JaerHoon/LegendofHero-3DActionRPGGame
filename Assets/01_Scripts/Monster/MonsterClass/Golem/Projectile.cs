using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int typeNum = 0;

    float deltaX;
    float deltaZ;

    // 나선형 운동의 속도
    public float speed = 0.8f;
    // 나선형의 증가 속도 (커지는 속도)
    public float spiralGrowthRate = 0.08f;

    // 시간 값을 저장할 변수
    private float time = 0.0f;

    public void Init()
    {
        spiralGrowthRate = 0.1f;
    }

    public void Settype(int num)
    {
        typeNum = num;
    }

    void Update()
    {
        // 시간 값을 증가시킴
        time += Time.deltaTime * speed;

        // 나선형의 x와 y 변위 계산
        switch (typeNum)
        {
            case 0://나선 순방향
                deltaX = Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
                deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;
                transform.Translate(new Vector3(deltaX, 0, deltaZ));
                break;
            case 1://나선 역방향
                deltaX = (-1) * Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
                deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;
                transform.Translate(new Vector3(deltaX, 0, deltaZ));
                break;
            case 2:// 직선 느림
                transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
                break;
            case 3:// 직선 빠름
                transform.Translate(Vector3.forward * 5.5f * Time.deltaTime);
                break;
            default:
                break;
        }
        
        // 물체의 위치를 Translate를 사용하여 업데이트

        // 나선형 반경을 점차적으로 증가시킴
        
        if(spiralGrowthRate > 9)
            spiralGrowthRate += Time.deltaTime * 7f;
        else
            spiralGrowthRate += Time.deltaTime * 3f;
    }
}
