using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float shakeDuration = 0f;
    private float shakeTimer = 0f;
    private float startingAmplitude;
    private float startingFrequency;

    void Start()
    {
        if (virtualCamera != null)
        {
            perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlinNoise != null)
            {
                startingAmplitude = perlinNoise.m_AmplitudeGain;
                startingFrequency = perlinNoise.m_FrequencyGain;
            }
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                StopShake();
            }
        }
    }

    public void StartShake(float duration, float amplitude, float frequency)
    {
        if (perlinNoise != null)
        {
            perlinNoise.m_AmplitudeGain = amplitude;
            perlinNoise.m_FrequencyGain = frequency;
            shakeDuration = duration;
            shakeTimer = duration;
        }
    }

    public void StopShake()
    {
        if (perlinNoise != null)
        {
            perlinNoise.m_AmplitudeGain = startingAmplitude;
            perlinNoise.m_FrequencyGain = startingFrequency;
        }
    }
}
