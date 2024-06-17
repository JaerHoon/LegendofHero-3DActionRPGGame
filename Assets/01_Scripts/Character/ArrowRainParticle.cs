using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainParticle : MonoBehaviour
{
    public static ArrowRainParticle instance;

    public ParticleSystem Aoe;
    public ParticleSystem circle;
    public ParticleSystem Light;

    public float DestroyDuration = 4.0f;
    public float DestroyLifetime = 2.0f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        Aoe = GetComponent<ParticleSystem>();
        circle = transform.GetChild(5).GetComponent<ParticleSystem>();
        Light = transform.GetChild(6).GetComponent<ParticleSystem>();
    }
    void Start()
    {
        
    }

    public void ParticleControl()
    {
        var main = Aoe.main;
        main.duration = 4.0f;

        var mainCircle = circle.main;
        mainCircle.startLifetime = 4.0f;

        var mainLight = Light.main;
        mainLight.startLifetime = 4.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
