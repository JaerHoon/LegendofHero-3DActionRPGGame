using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }

    InGameCanvasController inGameCanvasController;


    [SerializeField] GameObject pausePanel;//�Ͻ����� �г� ������
    private bool isPause = false;

    AudioSource myAudioSource;
    [SerializeField]
    AudioMixer audioMixer;

    public Slider audioSlider_Master;
    public Slider audioSlider_BGM;
    public Slider audioSlider_Effect;

    [SerializeField]
    AudioStorage soundStorage;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void OnSetPausePanel()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
    }

    public void AudioMasterControl()
    {
        float volume_Master = audioSlider_Master.value;
        audioMixer.SetFloat("Master", volume_Master);
        if (volume_Master == -40f) audioMixer.SetFloat("Master", -80f);

    }

    public void ChangeBGM(int StageType)//0�κ� 1,2�������� 3���� 4����
    {
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(soundStorage.SoundSrc[StageType].SoundFile);
    }

    public void AudioBGMControl()
    {
        float volume_BGM = audioSlider_BGM.value;
        audioMixer.SetFloat("BGM", volume_BGM);
        if (volume_BGM == -40f) audioMixer.SetFloat("BGM", -80f);
    }

    public void AudioEffectControl()
    {
        float volume_Effect = audioSlider_Effect.value;
        audioMixer.SetFloat("Effect", volume_Effect);
        if (volume_Effect == -40f) audioMixer.SetFloat("Effect", -80f);
    }

    int SoundChangeNum = 1;

    public void OnChangeAudioClip()
    {
        myAudioSource.Stop();
        myAudioSource.clip = soundStorage.SoundSrc[SoundChangeNum % soundStorage.SoundSrc.Length].SoundFile;
        myAudioSource.Play();
        SoundChangeNum++;
    }

    // Start is called before the first frame update
    void Start()
    {

        inGameCanvasController = FindAnyObjectByType<InGameCanvasController>();

        myAudioSource = GetComponent<AudioSource>();
        GameObject.FindFirstObjectByType<GameManager>().GetComponent<AudioSource>().Stop();
        //ChangeBGM(0);
    }


    void SetPausePanel()
    {
        inGameCanvasController.OffGameOverPanel();
        inGameCanvasController.OffStageClear();
        isPause = !isPause;
        int flagAsInt = isPause ? 0 : 1;
        Time.timeScale = flagAsInt;
        pausePanel.SetActive(isPause);
       
    }

    public void GameExitButton()
    {
        Application.Quit();
       
    }



    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPausePanel();
        }
    }
}
