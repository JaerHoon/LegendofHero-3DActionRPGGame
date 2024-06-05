using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMove : MonoBehaviour
{
    [SerializeField]
    float floatAmplitude = 0.1f; // 위아래로 움직이는 진폭
    [SerializeField]
    float floatFrequency = 1f; // 위아래로 움직이는 주기
    private Vector3 initialLocalPosition;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    private void LateUpdate()
    {

        // 위아래로 움직이는 애니메이션을 추가합니다.
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = initialLocalPosition + new Vector3(0, floatOffset, 0);
    }
}
