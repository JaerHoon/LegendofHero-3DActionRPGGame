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
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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


    [SerializeField] GameObject pausePanel;//일시정지 패널 프리팹
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
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

    public void ChangeBGM(int StageType)//0로비 1,2몬스테이지 3마켓 4보스
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
