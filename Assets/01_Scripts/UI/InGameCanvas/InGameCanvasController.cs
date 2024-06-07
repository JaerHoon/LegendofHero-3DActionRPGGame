using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasController : UIController
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().PlayerHp--;
        }
    }
}
