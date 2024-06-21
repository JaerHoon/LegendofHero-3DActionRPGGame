using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStagePortal : MonoBehaviour
{
    StageManager stageManager;
    InGameCanvasController inGameCanvasController;
    SphereCollider coll;
    bool IsInteraction;

    void Start()
    {
        stageManager = GameObject.FindFirstObjectByType<StageManager>();
        inGameCanvasController = GameObject.FindFirstObjectByType<InGameCanvasController>();
        coll = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInteraction= true;
            inGameCanvasController.OnInteraction("NextStage");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInteraction = false;
            inGameCanvasController.OffInteraction();
        }
    }

    private void Update()
    {
        if(IsInteraction == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
              
                inGameCanvasController.OffInteraction();
                IsInteraction = false;
                stageManager.EnterStage(stageManager.currentStageNum + 1);
               
            }
        }
    }
}
