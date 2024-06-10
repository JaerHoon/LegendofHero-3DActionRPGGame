using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DragNDrop : UI,IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private void Start()
    {
        Init();

        Ondrag = null;
        Dragging = null;
        OffDrag = null;
        Drop = null;

        SlotOndrag = null;
        SlotDragging = null;
        SlotOffDarg = null;
        SlotDrop = null;

        if(viewType == ViewType.DragNDrop)
        {
            viewController.GetMethod(uITypeNumber, ref Ondrag, SetectedValue);
            viewController.GetMethod(uITypeNumber, ref Dragging, SetectedValue1);
            viewController.GetMethod(uITypeNumber, ref OffDrag, SetectedValue2);
            viewController.GetMethod(uITypeNumber, ref Drop, SetectedValue3);
        }
        else if(viewType == ViewType.DragNDropSlot)
        {
            viewController.GetSlotMethod(uITypeNumber, ref SlotOndrag, SetectedValue, slotNumber);
            viewController.GetSlotMethod(uITypeNumber, ref SlotDragging, SetectedValue1, slotNumber);
            viewController.GetSlotMethod(uITypeNumber, ref SlotOffDarg, SetectedValue2, slotNumber);
            viewController.GetSlotMethod(uITypeNumber, ref SlotDrop, SetectedValue3, slotNumber);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (viewType == ViewType.DragNDrop)
        {
            Ondrag?.Invoke(eventData);
        }
        else if (viewType == ViewType.DragNDropSlot)
        {
            SlotOndrag?.Invoke(eventData, slotNumber);
        }
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (viewType == ViewType.DragNDrop)
        {
            OffDrag?.Invoke(eventData);
        }
        else if (viewType == ViewType.DragNDropSlot)
        {
            SlotOffDarg?.Invoke(eventData, slotNumber);
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (viewType == ViewType.DragNDrop)
        {
            Dragging?.Invoke(eventData);
        }
        else if (viewType == ViewType.DragNDropSlot)
        {
            SlotDragging?.Invoke(eventData, uITypeNumber);
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (viewType == ViewType.DragNDrop)
        {
            Drop?.Invoke(eventData);
        }
        else if (viewType == ViewType.DragNDropSlot)
        {
            SlotDrop?.Invoke(eventData, slotNumber);
        }

       
    }
}
