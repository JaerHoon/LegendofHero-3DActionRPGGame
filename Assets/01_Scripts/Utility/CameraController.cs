using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera fallowCam;
    public CinemachineVirtualCamera NPCcam;

    private void Awake()
    {
        fallowCam.m_Priority = 5;
        NPCcam.m_Priority = 10;
    }
    public void OnNPCdialog(Transform NPC)
    {

    }
}
