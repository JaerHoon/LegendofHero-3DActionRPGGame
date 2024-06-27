using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
     public Texture2D cursorTexture;  // 마우스 커서로 사용할 텍스처
    public Vector2 hotSpot = Vector2.zero;  // 커서의 핫스팟 위치 (기본값은 텍스처의 왼쪽 위)

    void Start()
    {
        // 커서를 변경합니다.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDisable()
    {
        // 스크립트가 비활성화될 때 기본 커서로 돌아갑니다.
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
