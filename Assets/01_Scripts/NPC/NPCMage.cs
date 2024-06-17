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
        startcoversations[0] = "안녕하신가! 힘세고 강한 아침!";
        startcoversations[1] = "만일 내게 물어보면 나는 메이지!";
        startcoversations[2] = "갑자기 마왕 쳐들어 왔다!";
        startcoversations[3] = "용사여!";
        startcoversations[4] = "해줘잉!";

        marketconversations[0] = "안녕하신가! 힘세고 강한 점심!";
        marketconversations[1] = "만일 내게 물어보면 나는 상인 메이지";
        marketconversations[2] = "골라골라! 싸다 싸!";
        marketconversations[3] = "돈 없으면 저리가!!";
        
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
