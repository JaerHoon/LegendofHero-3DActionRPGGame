using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSound : MonoBehaviour
{
    public static KnightSound instance;

    private void Awake()
    {
        if (KnightSound.instance == null)
            KnightSound.instance = this;
    }

    [SerializeField]
    AudioStorage soundStorage;

    AudioSource myaudio;

    void Start()
    {
        myaudio = GetComponent<AudioSource>();
    }

    public void OnKnightBaseAttackSound()
    {
        myaudio.PlayOneShot(soundStorage.SoundSrc[0].SoundFile);
    }

    public void OnKnightSkillSound()
    {
        myaudio.PlayOneShot(soundStorage.SoundSrc[1].SoundFile);
    }

    public void OnKnightSkillExSound()
    {
        myaudio.PlayOneShot(soundStorage.SoundSrc[2].SoundFile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
