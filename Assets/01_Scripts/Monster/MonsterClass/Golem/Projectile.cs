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

    // ������ ��� �ӵ�
    public float speed = 1f;
    // �������� ���� �ӵ� (Ŀ���� �ӵ�)
    public float spiralGrowthRate = 0.1f;

    // �ð� ���� ������ ����
    private float time = 0.0f;

    public void Init()
    {
        spiralGrowthRate = 0.1f;
    }

    void Update()
    {
        // �ð� ���� ������Ŵ
        time += Time.deltaTime * speed;

        // �������� x�� y ���� ���
        float deltaX = Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
        float deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;

        // ��ü�� ��ġ�� Translate�� ����Ͽ� ������Ʈ
        transform.Translate(new Vector3(deltaX, 0, deltaZ));

        // ������ �ݰ��� ���������� ������Ŵ
        
        if(spiralGrowthRate > 9)
            spiralGrowthRate += Time.deltaTime * 5f;
        else
            spiralGrowthRate += Time.deltaTime * 3f;
    }
}
