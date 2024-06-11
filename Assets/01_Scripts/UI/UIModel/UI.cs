using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public enum ViewType 
       { Image, ImageSlot, Text, 
        TextSlot, Button, ButtonSlot, DragNDrop, DragNDropSlot, Slider,
        CoolTime, CoolTimeSlot,  GameObjectm, GameObjectSlot, SlidetSlot }
    [HideInInspector]
    public ViewType viewType;
    [HideInInspector]
    public int slotNumber;
    [HideInInspector]
    public UIModel uIModel;

    public UIController viewController;
    [HideInInspector]
    public enum valueType { Field, Property}
    [HideInInspector]
    public valueType value1_type;
    [HideInInspector]
    public valueType value2_type;
   
    [HideInInspector]
    public int uITypeNumber;

    [HideInInspector]
    public string SetectedValue;
    [HideInInspector]
    public string SetectedValue1;
    [HideInInspector]
    public string SetectedValue2;
    [HideInInspector]
    public string SetectedValue3;
    [HideInInspector]
    public string valueText;
    [HideInInspector]
    public string valueText2;
    [HideInInspector]
    public object value;
    [HideInInspector]
    public object value2;
    [HideInInspector]
    public string SelectedValue1;

    protected Image image;
    protected Button button;
    protected TextMeshProUGUI textMesh;
    protected Slider slider;


    public UIController.Eventchain ButtonPressed;
    public UIController.DragEventchain Ondrag;
    public UIController.DragEventchain Dragging;
    public UIController.DragEventchain OffDrag;
    public UIController.DragEventchain Drop;
    public UIController.Eventchain CoolTime;

    public UIController.SlotEventchain SlotButtonPressed;
    public UIController.SlotDragEventchain SlotOndrag;
    public UIController.SlotDragEventchain SlotDragging;
    public UIController.SlotDragEventchain SlotOffDarg;
    public UIController.SlotDragEventchain SlotDrop;
    public UIController.SlotEventchain SlotCoolTime;


    protected void Init()
    {
        SetComopnent(viewType);
        uIModel.upDateUI += UIUpdate;
        uIModel.SlotCoolTimeStart += SlotstartCoolTime;
        uIModel.OneCoolTimeStart += OneStartCoolTiem;
    }

    protected void SetComopnent(ViewType view)
    {
        switch ((int)view)
        {
            case 0:
            case 1:
            case 6:
            case 7:
            case 9:
            case 10:
                TryGetComponent<Image>(out image);
                break;
            case 2:
            case 3:
                TryGetComponent<TextMeshProUGUI>(out textMesh);
                break;
            case 4:
            case 5:
                TryGetComponent<Button>(out button);
               
                break;
            case 8:
            case 13:
                TryGetComponent<Slider>(out slider);
                break;
        }
    }

    public virtual void UIUpdate()
    {
       
    }

    protected virtual object GetValue(string ValueName, int ValueNum)
    {
        object va = default;
        if (ValueNum == 1)
        {
            if (value1_type == valueType.Field)
            {
                va = viewController.GetValue(uITypeNumber, ValueName);
            }
            else
            {
                va = viewController.GetPropertyValue(uITypeNumber, ValueName);
            }
        }
        else if (ValueNum == 2) 
        {
            if (value2_type == valueType.Field)
            {
                va = viewController.GetValue(uITypeNumber, ValueName);
            }
            else
            {
                va = viewController.GetPropertyValue(uITypeNumber, ValueName);
            }
        }
      

        return va;
    }

    protected virtual object GetSlotValue(int ValueNum )
    {

        object va = default;
        if (ValueNum == 1)
        {
            if (value1_type == valueType.Field)
            {
                va = viewController.GetSlotValue(uITypeNumber, SetectedValue, SelectedValue1, slotNumber);
            }
            else
            {
                va = viewController.GetSlotPropertyValue(uITypeNumber, SetectedValue, SelectedValue1, slotNumber);
            }
        }
        else if (ValueNum == 2)
        {
            if (value2_type == valueType.Field)
            {
                va = viewController.GetSlotValue(uITypeNumber, SetectedValue, SelectedValue1, slotNumber);
            }
            else
            {
                va = viewController.GetSlotPropertyValue(uITypeNumber, SetectedValue, SelectedValue1, slotNumber);
            }
        }
        
        return va;
    }

    protected virtual void SlotstartCoolTime(float time, int slotnum)
    {

    }
    
    protected virtual void OneStartCoolTiem(float time)
    {

    }


}
