using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMage : MonoBehaviour
{
    public enum StageType { Start, Market}
    public StageType stageType;
    InGameCanvasController inGame;
    bool IsInteraction;

    private void Start()
    {
        inGame = GameObject.FindFirstObjectByType<InGameCanvasController>();
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
                print("대화시작");
                inGame.OffInteraction();
                IsInteraction = false;
            }
        }
    }
}
