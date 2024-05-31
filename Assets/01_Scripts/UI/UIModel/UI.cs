using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public enum ViewType { Image, ImageSlot, Text, TextSlot, Button, ButtonSlot, DragNDrop, DragNDropSlot, Slider }
    [HideInInspector]
    public ViewType viewType;
    [HideInInspector]
    public int slotNumber;
    [HideInInspector]
    public UIModel uIModel;

    public UIController viewController;

   
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
    public UIController.Eventchain Ondrag;
    public UIController.Eventchain Dragging;
    public UIController.Eventchain OffDrag;
    public UIController.Eventchain Drop;

    public UIController.SlotEventchain SlotButtonPressed;
    public UIController.SlotEventchain SlotOndrag;
    public UIController.SlotEventchain SlotDragging;
    public UIController.SlotEventchain SlotOffDarg;
    public UIController.SlotEventchain SlotDrop;


    protected void Init()
    {
        SetComopnent(viewType);
        uIModel.upDateUI += UIUpdate;
    }

    protected void SetComopnent(ViewType view)
    {
        switch ((int)view)
        {
            case 0:
            case 1:
            case 7:
            case 8:
                TryGetComponent<Image>(out image);
                break;
            case 3:
            case 4:
                TryGetComponent<TextMeshProUGUI>(out textMesh);
                break;
            case 5:
            case 6:
                TryGetComponent<Button>(out button);
                break;
            case 9:
                TryGetComponent<Slider>(out slider);
                break;
        }
    }

    public virtual void UIUpdate()
    {

    }



}
