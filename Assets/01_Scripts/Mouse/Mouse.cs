using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
     public Texture2D cursorTexture;  // ���콺 Ŀ���� ����� �ؽ�ó
    public Vector2 hotSpot = Vector2.zero;  // Ŀ���� �ֽ��� ��ġ (�⺻���� �ؽ�ó�� ���� ��)

    void Start()
    {
        // Ŀ���� �����մϴ�.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �⺻ Ŀ���� ���ư��ϴ�.
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
