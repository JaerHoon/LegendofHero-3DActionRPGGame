using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public float speed;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    speed = 3.0f;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //}

    // 나선형 운동의 속도
    public float speed = 1f;
    // 나선형의 증가 속도 (커지는 속도)
    public float spiralGrowthRate = 0.1f;

    // 시간 값을 저장할 변수
    private float time = 0.0f;

    public void Init()
    {
        spiralGrowthRate = 0.1f;
    }

    void Update()
    {
        // 시간 값을 증가시킴
        time += Time.deltaTime * speed;

        // 나선형의 x와 y 변위 계산
        float deltaX = Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
        float deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;

        // 물체의 위치를 Translate를 사용하여 업데이트
        transform.Translate(new Vector3(deltaX, 0, deltaZ));

        // 나선형 반경을 점차적으로 증가시킴
        
        if(spiralGrowthRate > 9)
            spiralGrowthRate += Time.deltaTime * 5f;
        else
            spiralGrowthRate += Time.deltaTime * 3f;
    }
}
