using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_AllType : UI
{
    private void Start()
    {
        Init();
       
        if(button != null)
        {
            if(viewType == ViewType.Button)
            {
                ButtonPressed = null;
                viewController.GetMethod(uITypeNumber, ref ButtonPressed, SetectedValue);
            }
            else if(viewType == ViewType.ButtonSlot)
            {
                SlotButtonPressed = null;
                viewController.GetSlotMethod(uITypeNumber, ref SlotButtonPressed, SetectedValue, slotNumber);
            }
          
            
            button.onClick.AddListener(OnClick);
        }

        UIUpdate();
    }

    public override void UIUpdate()
    {
        switch ((int)viewType)
        {
            case 0:
            case 1:
                ImageUPdate(); break;
            case 2:
            case 3:
                TextMeshProGUGIUpdate(); break;
            case 8:
            case 13:
                SliderUpdate(); break;
            case 9:
            case 10:
                OnCoolTime(); break;
          
         }
    }

    void ImageUPdate()
    {
        if(viewType == ViewType.Image)
        {
            value = GetValue(SetectedValue, 1);
        }
        else if(viewType == ViewType.ImageSlot)
        {
            value = GetSlotValue(1);
        }
        image.sprite = (Sprite)value;
    }

    void SliderUpdate()
    {
        if(viewType == ViewType.Slider)
        {
            value = GetValue(SetectedValue, 1);
            value2 = GetValue(SetectedValue1, 2);
        }
        else if(viewType == ViewType.SlidetSlot)
        {
            value = GetSlotValue(1);
            value2 = GetSlotValue(2);

        }
       

        slider.maxValue = value != null ? (float)value : 0;
        slider.value = value2 != null ? (float)value2 : 0;
    }

    void TextMeshProGUGIUpdate()
    {
        if(viewType == ViewType.Text)
        {
            value = GetValue(SetectedValue, 1);
        }
        else if(viewType == ViewType.TextSlot)
        {
            value = GetSlotValue(1);
        }
        
        textMesh.text = value?.ToString() ?? null;
    }

    void OnCoolTime()
    {
        if(viewType == ViewType.CoolTime)
        {
            value = GetValue(SetectedValue, 1);
        }
        else if(viewType == ViewType.CoolTimeSlot)
        {
            value = GetSlotValue(1);
        }

        if(value != null)
        {
            StopAllCoroutines();
            StartCoroutine(CoolTiem());
        }
       
      
    }

    IEnumerator CoolTiem()
    {
        image.fillAmount = 0;

        float time = 0;
        float cooltime = (float)value;

        while (time < cooltime)
        {
            image.fillAmount += (1 / cooltime * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void OnClick()
    {
        if(viewType == ViewType.Button)
        {
            ButtonPressed?.Invoke();
        }
        else if(viewType == ViewType.ButtonSlot)
        {
            SlotButtonPressed?.Invoke(slotNumber);
        }
        
    }

}
