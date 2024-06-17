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

    CharacterManager.ChoicedCharacter ChoicedCharacter;
  
    private void Start()
    {
        offSetCam.m_Priority = 10;
        choiceCam.m_Priority = 5;
        knightInfo.SetActive(false);
        ArcherInfo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChoicedCharacter = CharacterManager.ChoicedCharacter.Warrior;
            knightInfo.SetActive(true);
            ArcherInfo.SetActive(false);
            ChangeCharacter();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChoicedCharacter = CharacterManager.ChoicedCharacter.Archer;
            knightInfo.SetActive(false);
            ArcherInfo.SetActive(true);
            ChangeCharacter();
        }

        if (Input.GetKeyDown(KeyCode.E))
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

    }



}
