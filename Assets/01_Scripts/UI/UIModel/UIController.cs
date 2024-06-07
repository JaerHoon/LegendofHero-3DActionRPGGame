using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;



public class UIController : MonoBehaviour
{
   
    public List<UIModel> viewModels = new List<UIModel>();

    public delegate void Eventchain();
    public delegate void SlotEventchain(int parameter);
    public delegate void UpDateUI();

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
            object slot = ilist[Slotnum];
            FieldInfo slotField = elementType.GetField(valueName, BindingFlags.Public | BindingFlags.Instance);

            value = slotField.GetValue(slot);
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
}
