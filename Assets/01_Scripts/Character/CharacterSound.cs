using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    public static CharacterSound instance;

    private void Awake()
    {
        if (CharacterSound.instance == null)
            CharacterSound.instance = this;
    }

    [SerializeField]
    AudioStorage soundStorage_Knight;
    [SerializeField]
    AudioStorage soundStorage_Archer;

    AudioSource myaudio;

    void Start()
    {
        myaudio = GetComponent<AudioSource>();
    }

    public void OnKnightBaseAttackSound()
    {
        myaudio.PlayOneShot(soundStorage_Knight.SoundSrc[0].SoundFile);
    }

    public void OnKnightSkillSound()
    {
        myaudio.PlayOneShot(soundStorage_Knight.SoundSrc[1].SoundFile);
    }

    public void OnKnightSkillExSound()
    {
        myaudio.PlayOneShot(soundStorage_Knight.SoundSrc[2].SoundFile);
    }

    public void OnKnightShieldSound()
    {
        myaudio.PlayOneShot(soundStorage_Knight.SoundSrc[3].SoundFile);
    }

    public void OnArcherBaseAttackSound()
    {
        myaudio.PlayOneShot(soundStorage_Archer.SoundSrc[0].SoundFile);
    }

    public void OnArcherSkillSound()
    {
        myaudio.PlayOneShot(soundStorage_Archer.SoundSrc[1].SoundFile);
    }

    public void OnArcherShieldSound()
    {
        myaudio.PlayOneShot(soundStorage_Archer.SoundSrc[2].SoundFile);
    }

    public void OnArcherSkillHitSound()
    {
        myaudio.PlayOneShot(soundStorage_Archer.SoundSrc[3].SoundFile);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
