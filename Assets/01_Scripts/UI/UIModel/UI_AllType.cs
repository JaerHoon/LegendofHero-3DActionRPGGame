using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_AllType : UI
{

    Coroutine CurrentCorutine;
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
                image.type = Image.Type.Filled;
                image.fillAmount = 0;
                break;
            case 11:
            case 12:
                SetGameObject();
                break;
          
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
        
        if(value != null)
        {
            image.sprite = (Sprite)value;
        }
        else
        {
            image.sprite = default;
        }
       
       
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


    protected override void OneStartCoolTiem(float time)
    {
        if (viewType != ViewType.CoolTime) return;
        if (CurrentCorutine != null)
        {
            return;
        }
        else
        {
            CurrentCorutine = StartCoroutine(Cooltime(time));
        }
    }

    protected override void SlotstartCoolTime(float time, int slotnum)
    {
        if (viewType != ViewType.CoolTimeSlot) return;
        if(slotnum == slotNumber)
        {
            if (CurrentCorutine != null)
            {
                return;
            }
            else
            {
                CurrentCorutine = StartCoroutine(Cooltime(time));
            }
        }
       
    }

    IEnumerator Cooltime(float cooltime)
    {
        if(image == null)
        {
            print(this.gameObject.name + ": �̹��� ��");
        }
        image.fillAmount = 1;

        float time = 0;
        float Cooltime = cooltime;

        while (time < cooltime)
        {
            time += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(1, 0, time / cooltime); // ��Ÿ�� ���൵�� ���� fillAmount�� ����
            yield return null;
        }

        image.fillAmount = 0;
        CurrentCorutine = null;
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

    void SetGameObject()
    {
        if (viewType == ViewType.GameObject)
        {
            viewController.GameObjectSet(uITypeNumber, SetectedValue, this.gameObject);
        }
        else if(viewType == ViewType.GameObjectSlot)
        {
            viewController.SlotGameObjectSet(uITypeNumber, SetectedValue, SetectedValue1, this.gameObject, slotNumber);
        }   
    }

}
