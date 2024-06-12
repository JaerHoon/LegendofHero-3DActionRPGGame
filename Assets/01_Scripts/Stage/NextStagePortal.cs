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

    private void Update()
    {
        if(IsInteraction == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("대화시작");
                inGameCanvasController.OffInteraction();
                IsInteraction = false;
                stageManager.SetStage(stageManager.currentStageNum + 1);
            }
        }
    }
}
