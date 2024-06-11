using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine.EventSystems;



public class UIController : MonoBehaviour
{
   
    public List<UIModel> viewModels = new List<UIModel>();

    public delegate void Eventchain();
    public delegate void DragEventchain(PointerEventData eventData);
    public delegate void SlotDragEventchain(PointerEventData eventData, int parameter);
    public delegate void SlotEventchain(int parameter);
    public delegate void UpDateUI();
    public delegate void SlotCoolTime(float time, int slotnum);
    public delegate void OneCoolTime(float time);

    public virtual object GetValue(int viewModelNum, string valueName)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                  .FirstOrDefault(f => f.Name == valueName);

        object value = field.GetValue(viewModel);

        return value;
       
    }

    public virtual object GetSlotValue(int viewModelNum, string listName, string valueName, int Slotnum)
    {
        object value = default;

        
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(f => f.Name == listName);

        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
        {
            object list = field.GetValue(viewModel);

            Type listType = list.GetType();

            Type elementType = listType.GetGenericArguments()[0];
            IList ilist = list as IList;
            if(ilist.Count > Slotnum)
            {
                object slot = ilist[Slotnum];
                FieldInfo slotField = elementType.GetField(valueName, BindingFlags.Public | BindingFlags.Instance);

                value = slotField.GetValue(slot);
               
            }
            else
            {
                value = null;
            }
          
        }

        return value;
    }

    public virtual void GetMethod(int viewModelNum, ref Eventchain eventchain, string MethodName)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        MethodInfo method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Instance);

        if (method != null)
        {
            Delegate del = Delegate.CreateDelegate(typeof(Eventchain), viewModel, method);

            eventchain += (Eventchain)del;
        }

    }

    public virtual void GetMethod(int viewModelNum, ref DragEventchain eventchain, string MethodName)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        MethodInfo method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Instance);

        if (method != null)
        {
            Delegate del = Delegate.CreateDelegate(typeof(DragEventchain), viewModel, method);

            eventchain += (DragEventchain)del;
        }

    }

    public virtual void GetSlotMethod(int viewModelNum, ref SlotEventchain eventchain, string MethodName, int slotnum)
    {
      
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        MethodInfo method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Instance);

        if (method != null)
        {
            Delegate del = Delegate.CreateDelegate(typeof(SlotEventchain), viewModel, method);

            eventchain += (SlotEventchain)del;
        }
    }

    public virtual void GetSlotMethod(int viewModelNum, ref SlotDragEventchain eventchain, string MethodName, int slotnum)
    {

        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        MethodInfo method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Instance);

        if (method != null)
        {
            Delegate del = Delegate.CreateDelegate(typeof(SlotDragEventchain), viewModel, method);

            eventchain += (SlotDragEventchain)del;
        }
    }
    public virtual void ChainUpDateUI()
    {

    }

    public virtual void GameObjectSet(int viewModelNum, string fieldName, GameObject gameObject)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                  .FirstOrDefault(f => f.Name == fieldName);
        field.SetValue(viewModel, gameObject);
    }

    public virtual void SlotGameObjectSet(int viewModelNum, string listName, string fieldName, GameObject gameObject, int slotnum)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();
        FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(f => f.Name == listName);

        if (field != null && field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
        {
            object list = field.GetValue(viewModel);

            IList ilist = list as IList;
            if (ilist != null && ilist.Count > slotnum)
            {
                ilist[slotnum] = gameObject;
            }
        }
    }

    public virtual object GetSlotPropertyValue(int viewModelNum, string listName, string valueName, int slotNum)
    {
        object value = default;

        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();

        // 리스트 프로퍼티 가져오기
        FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                .FirstOrDefault(f => f.Name == listName);

        if(field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
        {
            // 리스트 프로퍼티의 값을 가져오기
            IList list = (IList)field.GetValue(viewModel);

            // 리스트 요소 가져오기
            if (slotNum >= 0 && slotNum < list.Count)
            {
                object slot = list[slotNum];

                // 슬롯의 타입 가져오기
                Type slotType = slot.GetType();

                // 프로퍼티 가져오기
                PropertyInfo valueProperty = slotType.GetProperty(valueName, BindingFlags.Public | BindingFlags.Instance);

                if (valueProperty != null)
                {
                    // 프로퍼티의 값을 가져오기
                    value = valueProperty.GetValue(slot);
                }
            }
        }

        return value;
    }

    public virtual object GetPropertyValue(int viewModelNum, string valueName)
    {
        UIModel viewModel = viewModels[viewModelNum];
        Type type = viewModel.GetType();

        // 필드가 아니라 프로퍼티를 가져옴
        PropertyInfo property = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .FirstOrDefault(p => p.Name == valueName);

        if (property != null)
        {
            object value = property.GetValue(viewModel);
            return value;
        }
        else
        {
            // 프로퍼티가 존재하지 않는 경우에 대한 처리
            return null;
        }
    }
}
