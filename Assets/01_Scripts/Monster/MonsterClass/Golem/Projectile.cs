using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int typeNum = 0;

    float deltaX;
    float deltaZ;

    // ������ ��� �ӵ�
    public float speed = 0.8f;
    // �������� ���� �ӵ� (Ŀ���� �ӵ�)
    public float spiralGrowthRate = 0.08f;

    // �ð� ���� ������ ����
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
        // �ð� ���� ������Ŵ
        time += Time.deltaTime * speed;

        // �������� x�� y ���� ���
        switch (typeNum)
        {
            case 0://���� ������
                deltaX = Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
                deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;
                transform.Translate(new Vector3(deltaX, 0, deltaZ));
                break;
            case 1://���� ������
                deltaX = (-1) * Mathf.Cos(time) * spiralGrowthRate * Time.deltaTime;
                deltaZ = Mathf.Sin(time) * spiralGrowthRate * Time.deltaTime;
                transform.Translate(new Vector3(deltaX, 0, deltaZ));
                break;
            case 2:// ���� ����
                transform.Translate(Vector3.forward * 3.0f * Time.deltaTime);
                break;
            case 3:// ���� ����
                transform.Translate(Vector3.forward * 5.5f * Time.deltaTime);
                break;
            default:
                break;
        }
        
        // ��ü�� ��ġ�� Translate�� ����Ͽ� ������Ʈ

        // ������ �ݰ��� ���������� ������Ŵ
        
        if(spiralGrowthRate > 9)
            spiralGrowthRate += Time.deltaTime * 7f;
        else
            spiralGrowthRate += Time.deltaTime * 3f;
    }
}
