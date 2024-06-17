using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMage : MonoBehaviour
{
    public enum StageType { Start, Market}
    public StageType stageType;
    InGameCanvasController inGame;
    bool IsInteraction;
    string[] startcoversations = new string[5];
    string[] marketconversations = new string[4];

    private void Start()
    {
        inGame = GameObject.FindFirstObjectByType<InGameCanvasController>();
        startcoversations[0] = "�ȳ��ϽŰ�! ������ ���� ��ħ!";
        startcoversations[1] = "���� ���� ����� ���� ������!";
        startcoversations[2] = "���ڱ� ���� �ĵ�� �Դ�!";
        startcoversations[3] = "��翩!";
        startcoversations[4] = "������!";

        marketconversations[0] = "�ȳ��ϽŰ�! ������ ���� ����!";
        marketconversations[1] = "���� ���� ����� ���� ���� ������";
        marketconversations[2] = "�����! �δ� ��!";
        marketconversations[3] = "�� ������ ������!!";
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            inGame.OnInteraction("conversation");
           
            IsInteraction = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inGame.OffInteraction();
            IsInteraction = false;

        }
    }

    private void Update()
    {
        if (IsInteraction == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnDailog();
                inGame.OffInteraction();
                IsInteraction = false;
            }
        }
    }

    void OnDailog()
    {
        if(stageType == StageType.Start)
        {
            inGame.OnDialog(startcoversations, this.gameObject.transform);
        }
        else
        {
            inGame.OnDialog(marketconversations, this.gameObject.transform);
        }
    }
}
