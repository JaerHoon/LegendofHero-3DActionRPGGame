using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_NormalATK : Platform
{
    InGameCanvasController InGameCanvas;
    

    private void Start()
    {
        InGameCanvas = GameObject.FindAnyObjectByType<InGameCanvasController>();
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameCanvas.OnskillInfo(skill);
        }
    }
}
