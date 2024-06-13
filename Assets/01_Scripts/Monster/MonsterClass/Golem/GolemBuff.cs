using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBuff : MonsterBuff
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            OnPoison(10, 1, 5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            OnCurse(5,0,0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Onfreeze(7,0,0);
        }
    }
}
