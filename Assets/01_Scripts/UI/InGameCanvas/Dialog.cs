using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : UIModel
{
    InGameCanvasController inGame;
    public string dialog;
    bool IsNextKey = false;

    public GameObject EKey;
    int Maxoder;
    int oder;
    string[] texts;
    Coroutine coroutine;

    private void Awake()
    {
        inGame = FindFirstObjectByType<InGameCanvasController>();
    }
    public void OnDialog(string[] text)
    {
        texts = text;
        Maxoder = text.Length;
        oder = 0;
        StartCoroutine(Dialogging(texts[oder]));
    }

    IEnumerator Dialogging(string text)
    {
        EKey.SetActive(false);
        yield return new WaitForSeconds(1f);

        for (int i = 1; i <= text.Length; i++)
        {
            dialog = text.Substring(0, i);
            //print(dialog);
            ChangeUI();
            yield return new WaitForSeconds(0.1f);
        }
        IsNextKey = true;
       StartCoroutine(NextKey());
      
    }

    IEnumerator NextKey()
    {
        while (IsNextKey)
        {
            EKey.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            EKey.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
    }

    void NextDialog()
    {
        oder++; 
        if(oder <= Maxoder-1)
        {
            StartCoroutine(Dialogging(texts[oder]));
        }
        else
        {
            inGame.OffDialog();
        }
       
    }

    private void Update()
    {
        if(IsNextKey == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IsNextKey = false;
                NextDialog();
            }
        }
    }
}
