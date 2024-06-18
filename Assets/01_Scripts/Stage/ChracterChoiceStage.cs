using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChracterChoiceStage : UIController
{
    public Transform worrior;
    public Transform archer;

    public CinemachineVirtualCamera offSetCam;
    public CinemachineVirtualCamera choiceCam;

    public GameObject knightInfo;
    public GameObject ArcherInfo;
    public GameObject SelectPanel;

    CharacterManager.ChoicedCharacter ChoicedCharacter;
  
    private void Start()
    {
        offSetCam.m_Priority = 10;
        choiceCam.m_Priority = 5;
        knightInfo.SetActive(false);
        ArcherInfo.SetActive(false);
        SelectPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChoicedCharacter = CharacterManager.ChoicedCharacter.Warrior;
            CharacterManager.instance.choicedCharacter = ChoicedCharacter;
            knightInfo.SetActive(true);
            ArcherInfo.SetActive(false);
            SelectPanel.SetActive(true);
            ChangeCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChoicedCharacter = CharacterManager.ChoicedCharacter.Archer;
            CharacterManager.instance.choicedCharacter = ChoicedCharacter;
            knightInfo.SetActive(false);
            ArcherInfo.SetActive(true);
            SelectPanel.SetActive(true);
            ChangeCharacter();
        }

        if (Input.GetKeyDown(KeyCode.E) && SelectPanel.activeSelf == true)
        {
            CharacterManager.instance.choicedCharacter = ChoicedCharacter;
            Onstart();
        }
    }

    void ChangeCharacter()
    {
        choiceCam.m_Priority = 10;
        offSetCam.m_Priority = 5;

        if(ChoicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            ChangeCam(worrior);
        }
        else
        {
            ChangeCam(archer);
        }
    }

    void ChangeCam(Transform tr)
    {
        choiceCam.m_LookAt = tr;
        choiceCam.m_Follow = tr;
    }
    
    void Onstart()
    {
        GameManager.instance.OnPlayerStage();
    }



}
