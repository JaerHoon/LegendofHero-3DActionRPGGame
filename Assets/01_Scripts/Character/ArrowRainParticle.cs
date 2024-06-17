using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainParticle : MonoBehaviour
{
    

    public ParticleSystem Aoe;
    public ParticleSystem circle;
    public ParticleSystem Light;

    public float DestroyDuration = 4.0f;
    public float DestroyLifetime = 2.0f;
<<<<<<< HEAD
    
=======
    private void Awake()
    {
        if (instance == null)
            instance = this;
        Aoe = GetComponent<ParticleSystem>();
        circle = transform.GetChild(5).GetComponent<ParticleSystem>();
        Light = transform.GetChild(6).GetComponent<ParticleSystem>();
    }
>>>>>>> 8bc1200766ea6e3f33677732cd38e736c414ce88
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

    public void ParticleColor()
    {
        if (ArcherAttack.instance.isSkillSetting1)
        {
            var mainAoe = Aoe.main;
            mainAoe.startColor = new Color(51.0f / 255.0f, 117.0f / 255.0f, 166.0f / 255.0f);


            var CircleColor = circle.main;
            CircleColor.startColor = new Color(51.0f / 255.0f, 117.0f / 255.0f, 166.0f / 255.0f);


            var LightColor = Light.main;
            LightColor.startColor = new Color(51.0f / 255.0f, 117.0f / 255.0f, 166.0f / 255.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
