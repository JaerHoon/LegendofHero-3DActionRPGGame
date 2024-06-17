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
        fallowCam.m_Priority = 10;
        NPCcam.m_Priority = 5;
        NPCcam.gameObject.SetActive(false);
    }
    public void OnNPCdialog(Transform NPC)
    {
        NPCcam.gameObject.SetActive(true);
        NPCcam.m_Follow = NPC;
        NPCcam.m_LookAt = NPC;
        fallowCam.m_Priority = 5;
        NPCcam.m_Priority = 10;
    }

    public void Offdialog()
    {
        fallowCam.m_Priority = 10;
        NPCcam.m_Priority = 5;
        NPCcam.gameObject.SetActive(false);
    }
}
