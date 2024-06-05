using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMove : MonoBehaviour
{
    [SerializeField]
    float floatAmplitude = 0.1f; // ���Ʒ��� �����̴� ����
    [SerializeField]
    float floatFrequency = 1f; // ���Ʒ��� �����̴� �ֱ�
    private Vector3 initialLocalPosition;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    private void LateUpdate()
    {

        // ���Ʒ��� �����̴� �ִϸ��̼��� �߰��մϴ�.
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = initialLocalPosition + new Vector3(0, floatOffset, 0);
    }
}
