using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(UI), true)] // View 클래스의 객체의 에디터에 적용, 및 자식클래스에도 적용
public class ViewEditor : Editor
{
    UI viewtarget;


    public override void OnInspectorGUI()
    {
        viewtarget = (UI)target; // 에디터를 적용할 대상을 정한다.

        viewtarget.viewController = (UIController)EditorGUILayout.ObjectField("UIController", viewtarget.viewController, typeof(UIController), true);

        viewtarget.viewType = (UI.ViewType)EditorGUILayout.EnumPopup("UIType", viewtarget.viewType);

        if (viewtarget.viewController == null) return;
        if (viewtarget.viewController.viewModels.Count > 0)
        {
            string[] viewModelName = new string[viewtarget.viewController.viewModels.Count];
            for (int i = 0; i < viewtarget.viewController.viewModels.Count; i++)
            {
                viewModelName[i] = viewtarget.viewController.viewModels[i].GetType().Name;
              
            }

            viewtarget.uITypeNumber = EditorGUILayout.Popup("UIModelName", viewtarget.uITypeNumber, viewModelName);


            UIModel uIModel = viewtarget.viewController.viewModels[viewtarget.uITypeNumber];

            viewtarget.uIModel = uIModel;

            Type selectedtype = uIModel.GetType();

            if (selectedtype != null)
            {
                switch ((int)viewtarget.viewType)
                {
                    case 0: ImageType(selectedtype); break;
                    case 1: ImageSlotType(selectedtype); break;
                    case 2: TextType(selectedtype); break;
                    case 3: TextSlotType(selectedtype); break;
                    case 4: ButtonType(selectedtype); break;
                    case 5: ButtonSlotType(selectedtype); break;
                    case 6: DragNDrop(selectedtype); break;
                    case 7: DragNDropSlotType(selectedtype); break;
                    case 8: SliderType(selectedtype); break;
                    case 9: CoolTimeType(selectedtype); break;
                    case 10: SlotCoolTime(selectedtype); break;
                    case 11: GameObjectType(selectedtype); break;
                    case 12: SlotGameObject(selectedtype); break;
                    case 13: SlotSliderType(selectedtype); break;


                }

            }

        }


        //ViewController.UIType 이넘을 인스팩터에 표시해라."UI_Type" 라는 이름으로...
        // viewtarget.uIType = (UIController.UIType)EditorGUILayout.EnumPopup("UIModelName", viewtarget.uIType);



        //viewtarget.uIType에서 선택된 이넘 값과 이름을 같은 클래스를 찾아서 Type를 저장



        if (GUI.changed)
        {
            EditorUtility.SetDirty(viewtarget);
        }

    }


    void ImageType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] spriteFields = field.Where(field => field.FieldType == typeof(Sprite)).Select(field => field.Name).ToArray();
        PropertyInfo[] property = selectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        string[] propertyName = property.Where(pro => pro.PropertyType == typeof(Sprite)).Select(pro => pro.Name).ToArray();

        string[] combinedArray = spriteFields.Concat(propertyName).ToArray();


        if (combinedArray.Length > 0)
        {
            string[] fieldNames = new string[combinedArray.Length];
            for (int i = 0; i < combinedArray.Length; i++)
            {
                fieldNames[i] = combinedArray[i];
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("SpritName", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = default;
            if (spriteFields.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Field;
                 va = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);

            }
            else if (propertyName.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Property;
                 va = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);
            }
            
            Sprite sp = va as Sprite;

            if (sp != null)
            {
                viewtarget.value = va;
                viewtarget.valueText = sp.name;
                viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
            }
            else
            {
                viewtarget.valueText = "null";
                viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
            }



        }



    }

    void ImageSlotType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] listFieldNames = field
            .Where(field => field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            .Select(field => field.Name)
            .ToArray();

        if (listFieldNames.Length > 0)
        {
            int selectedIndex = Array.IndexOf(listFieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("SLotList", selectedIndex, listFieldNames);

            viewtarget.SetectedValue = listFieldNames[selectedIndex];

            viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

            FieldInfo selectField = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(f => f.Name == viewtarget.SetectedValue);

            Type fieldtype = selectField.FieldType;

            Type elementType = fieldtype.GetGenericArguments()[0];

            FieldInfo[] listField = elementType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] listproperty = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> spriteFields = listField
                                      .Where(listField => listField.FieldType == typeof(Sprite))
                                      .Select(listField => listField.Name)
                                      .ToList();

            List<string> spritePropertyNames = listproperty
                .Where(property => property.PropertyType == typeof(Sprite))
                .Select(property => property.Name)
                .ToList();

            List<string> combinedList = new List<string>();
            combinedList.AddRange(spriteFields);
            combinedList.AddRange(spritePropertyNames);



            if (combinedList.Count > 0)
            {
                int selectedIndex1 = combinedList.IndexOf(viewtarget.SelectedValue1);
                if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex1 = EditorGUILayout.Popup("SpritName", selectedIndex1, combinedList.ToArray());

                viewtarget.SelectedValue1 = combinedList[selectedIndex1];

                object va = default;

                if (spriteFields.Contains(viewtarget.SelectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Field;
                    va = viewtarget.viewController.GetSlotValue(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.SelectedValue1, viewtarget.slotNumber);

                   
                }
                else if (spritePropertyNames.Contains(viewtarget.SelectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Property;
                    va = viewtarget.viewController.GetSlotPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.SelectedValue1,viewtarget.slotNumber);
                }

                Sprite sp = va as Sprite;

                if (sp != null)
                {
                    viewtarget.value = va;
                    viewtarget.valueText = sp.name;
                    viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
                }
                else
                {
                    viewtarget.valueText = "null";
                    viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
                }




            }


        }

    }

    void TextType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] nonSpriteFields = field.Where(field =>
            field.FieldType == typeof(int) ||
            field.FieldType == typeof(float) ||
            field.FieldType == typeof(string)|| (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>) && (
                    field.FieldType.GetGenericArguments()[0] == typeof(int) ||
                    field.FieldType.GetGenericArguments()[0] == typeof(float)
                ))).Select(field => field.Name)
           .ToArray();

        PropertyInfo[] property = selectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        string[] propertyName = property.Where(property =>
            property.PropertyType == typeof(int) ||
            property.PropertyType == typeof(float) ||
            property.PropertyType == typeof(string) ||
                (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 (property.PropertyType.GetGenericArguments()[0] == typeof(int) || property.PropertyType.GetGenericArguments()[0] == typeof(float)))).Select(pro => pro.Name)
        .ToArray();

        string[] combinedArray = nonSpriteFields.Concat(propertyName).ToArray();

        if (combinedArray.Length > 0)
        {
            string[] fieldNames = new string[combinedArray.Length];
            for (int i = 0; i < combinedArray.Length; i++)
            {
                fieldNames[i] = combinedArray[i];
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("ValueName", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = default;

            if (nonSpriteFields.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Field;
                va = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);
            }
            else if (propertyName.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Property;
                va = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);
            }
            
           
            viewtarget.value = va;
            int? intva = va as int?;
            if(intva != null)
            {
                viewtarget.valueText = va.ToString();
                viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
            }
            else
            {
                viewtarget.valueText = "Null";
                viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
            }
            
           
         
        }

    }

    void TextSlotType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
         
        string[] listFieldNames = field
            .Where(fiel => fiel.FieldType.IsGenericType && fiel.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            .Select(fiel => fiel.Name)
            .ToArray();

        if (listFieldNames.Length > 0)
        {
            int selectedIndex = Array.IndexOf(listFieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("SLotList", selectedIndex, listFieldNames);

            viewtarget.SetectedValue = listFieldNames[selectedIndex];

            viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

            FieldInfo selectField = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(f => f.Name == viewtarget.SetectedValue);

            Type fieldtype = selectField.FieldType;

            Type elementType = fieldtype.GetGenericArguments()[0];


            FieldInfo[] listField = elementType.GetFields(BindingFlags.Public | BindingFlags.Instance).Where(elementType =>
            elementType.FieldType == typeof(int) ||
            elementType.FieldType == typeof(float) ||
            elementType.FieldType == typeof(string) ||
            (elementType.FieldType.IsGenericType && elementType.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>) && 
            (elementType.FieldType.GetGenericArguments()[0] == typeof(int) ||
                    elementType.FieldType.GetGenericArguments()[0] == typeof(float))))
           .ToArray();

            string[] FieldName = listField.Select(listField => listField.Name).ToArray();

            PropertyInfo[] property = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property =>
            property.PropertyType == typeof(int) ||
            property.PropertyType == typeof(float) ||
            property.PropertyType == typeof(string) ||
                (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 (property.PropertyType.GetGenericArguments()[0] == typeof(int) || 
                 property.PropertyType.GetGenericArguments()[0] == typeof(float))))
                   .ToArray();

            string[] propertyName = property.Select(pro => pro.Name).ToArray();

            string[] combinedName = FieldName.Concat(propertyName).ToArray();

            if (combinedName.Length > 0)
            {
                int selectedIndex1 = Array.IndexOf(combinedName, viewtarget.SelectedValue1);
                if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex1 = EditorGUILayout.Popup("ValueName", selectedIndex1, combinedName);

                viewtarget.SelectedValue1 = combinedName[selectedIndex1];

                object obj = default;

                if (FieldName.Contains(viewtarget.SelectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Field;
                    obj = viewtarget.viewController.GetSlotValue(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.SelectedValue1, viewtarget.slotNumber);
                }
                else if(propertyName.Contains(viewtarget.SelectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Property;
                    obj = viewtarget.viewController.GetSlotPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.SelectedValue1, viewtarget.slotNumber);
                }

                viewtarget.value = obj;
                viewtarget.valueText = obj.ToString();
                viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);
               
            }

        }



    }

    void ButtonType(Type selectedType)
    {
        MethodInfo[] methods = selectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        if (methods.Length > 0)
        {

            string[] MethodNames = new string[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                MethodNames[i] = methods[i].Name;
            }

            int selectedIndex = Array.IndexOf(MethodNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("MethodName", selectedIndex, MethodNames);

            viewtarget.SetectedValue = MethodNames[selectedIndex];

            viewtarget.ButtonPressed = null;

            viewtarget.viewController.GetMethod(viewtarget.uITypeNumber, ref viewtarget.ButtonPressed, viewtarget.SetectedValue);


        }

    }

    void ButtonSlotType(Type selectedType)
    {
        viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

        MethodInfo[] methods = selectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        if (methods.Length > 0)
        {
            string[] MethodNames = new string[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                MethodNames[i] = methods[i].Name;
            }

            int selectedIndex = Array.IndexOf(MethodNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("MethodName", selectedIndex, MethodNames);

            viewtarget.SetectedValue = MethodNames[selectedIndex];

            viewtarget.SlotButtonPressed = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uITypeNumber, ref viewtarget.SlotButtonPressed, viewtarget.SetectedValue, viewtarget.slotNumber);
        }



    }

    void DragNDrop(Type selectedType)
    {
        MethodInfo[] methods = selectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        if (methods.Length > 0)
        {
            string[] MethodNames = new string[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                MethodNames[i] = methods[i].Name;
            }

            int selectedIndex = Array.IndexOf(MethodNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("OnDragMethod", selectedIndex, MethodNames);

            viewtarget.SetectedValue = MethodNames[selectedIndex];

            viewtarget.Ondrag = null;

            viewtarget.viewController.GetMethod(viewtarget.uITypeNumber, ref viewtarget.Ondrag, viewtarget.SetectedValue);

            int selectedIndex1 = Array.IndexOf(MethodNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("DraggingMethod", selectedIndex1, MethodNames);

            viewtarget.SetectedValue1 = MethodNames[selectedIndex1];

            viewtarget.Dragging = null;

            viewtarget.viewController.GetMethod(viewtarget.uITypeNumber, ref viewtarget.Dragging, viewtarget.SetectedValue1);

            int selectedIndex2 = Array.IndexOf(MethodNames, viewtarget.SetectedValue2);
            if (selectedIndex2 == -1) selectedIndex2 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex2 = EditorGUILayout.Popup("EndDragMethod", selectedIndex2, MethodNames);

            viewtarget.SetectedValue2 = MethodNames[selectedIndex2];

            viewtarget.OffDrag = null;

            viewtarget.viewController.GetMethod(viewtarget.uITypeNumber, ref viewtarget.OffDrag, viewtarget.SetectedValue2);

            int selectedIndex3 = Array.IndexOf(MethodNames, viewtarget.SetectedValue3);
            if (selectedIndex3 == -1) selectedIndex3 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex3 = EditorGUILayout.Popup("DropMethod", selectedIndex3, MethodNames);

            viewtarget.SetectedValue3 = MethodNames[selectedIndex3];

            viewtarget.Drop = null;

            viewtarget.viewController.GetMethod(viewtarget.uITypeNumber, ref viewtarget.Drop, viewtarget.SetectedValue3);


        }


    }

    void DragNDropSlotType(Type selectedType)
    {

        viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

        MethodInfo[] methods = selectedType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        if (methods.Length > 0)
        {
            string[] MethodNames = new string[methods.Length];
            for (int i = 0; i < methods.Length; i++)
            {
                MethodNames[i] = methods[i].Name;
            }


            int selectedIndex = Array.IndexOf(MethodNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("OnDragMethod", selectedIndex, MethodNames);

            viewtarget.SetectedValue = MethodNames[selectedIndex];

            viewtarget.SlotOndrag = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uITypeNumber, ref viewtarget.SlotOndrag, viewtarget.SetectedValue, viewtarget.slotNumber);

            int selectedIndex1 = Array.IndexOf(MethodNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("DraggingMethod", selectedIndex1, MethodNames);

            viewtarget.SetectedValue1 = MethodNames[selectedIndex1];

            viewtarget.SlotDragging = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uITypeNumber, ref viewtarget.SlotDragging, viewtarget.SetectedValue1, viewtarget.slotNumber);

            int selectedIndex2 = Array.IndexOf(MethodNames, viewtarget.SetectedValue2);
            if (selectedIndex2 == -1) selectedIndex2 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex2 = EditorGUILayout.Popup("EndDragMethod", selectedIndex2, MethodNames);

            viewtarget.SetectedValue2 = MethodNames[selectedIndex2];

            viewtarget.SlotOffDarg = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uITypeNumber, ref viewtarget.SlotOffDarg, viewtarget.SetectedValue2, viewtarget.slotNumber);

            int selectedIndex3 = Array.IndexOf(MethodNames, viewtarget.SetectedValue3);
            if (selectedIndex3 == -1) selectedIndex3 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex3 = EditorGUILayout.Popup("DropMethod", selectedIndex3, MethodNames);

            viewtarget.SetectedValue3 = MethodNames[selectedIndex3];

            viewtarget.SlotDrop = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uITypeNumber, ref viewtarget.SlotDrop, viewtarget.SetectedValue3, viewtarget.slotNumber);

        }



    }

    void SliderType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        string[] fieldName = field.Where(field =>
            field.FieldType == typeof(int) ||
            field.FieldType == typeof(float))
            .Select(field => field.Name)
           .ToArray();

        PropertyInfo[] property = selectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        string[] propertyName = property.Where(property =>
            property.PropertyType == typeof(int) ||
            property.PropertyType == typeof(float)).Select(pro => pro.Name)
        .ToArray();

        string[] conbinedArray = fieldName.Concat(propertyName).ToArray();


        if (conbinedArray.Length > 0)
        {
            string[] fieldNames = new string[conbinedArray.Length];
            for (int i = 0; i < conbinedArray.Length; i++)
            {
                fieldNames[i] = conbinedArray[i];

            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("MaxValue", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = default;

            if (fieldName.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Field;
                va = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);
            }
            else if (propertyName.Contains(viewtarget.SetectedValue))
            {
                viewtarget.value1_type = UI.valueType.Property;
                va = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue);
            }

                viewtarget.value = va;
                viewtarget.valueText = va.ToString();
                viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);
         
          

            int selectedIndex1 = Array.IndexOf(fieldNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("CurrentValue", selectedIndex1, fieldNames);

            viewtarget.SetectedValue1 = fieldNames[selectedIndex1];

            object va2 = default;

            if (fieldName.Contains(viewtarget.SetectedValue1))
            {
                viewtarget.value2_type = UI.valueType.Field;
                va2 = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue1);
            }
            else if (propertyName.Contains(viewtarget.SetectedValue1))
            {
                viewtarget.value2_type = UI.valueType.Property;
                va2 = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue1);
            }

                viewtarget.value2 = va2;
                viewtarget.valueText2 = va.ToString();

                viewtarget.valueText2 = EditorGUILayout.TextField("Value2", viewtarget.valueText);
      
           
        }
    }

    void CoolTimeType(Type selectedType)
    {
       

    }

    void SlotCoolTime(Type selectedType)
    {
        viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

       
    }

    void GameObjectType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] gameObjectFields = field
           .Where(field => field.FieldType == typeof(GameObject))
           .ToArray();

        if (gameObjectFields.Length > 0)
        {
            string[] fieldNames = new string[gameObjectFields.Length];
            for (int i = 0; i < gameObjectFields.Length; i++)
            {
                fieldNames[i] = gameObjectFields[i].Name;
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("ValueName", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            viewtarget.viewController.GameObjectSet(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.gameObject);
        }
    }

    void SlotGameObject(Type selectedType)
    {
        viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] filteredFields = field.Where(field => field.FieldType != typeof(Sprite) &&
            !typeof(Delegate).IsAssignableFrom(field.FieldType))
            .ToArray();

        // 두 번째 필터링: 제네릭 List<> 타입의 필드 이름을 선택합니다.
        string[] listFieldNames = filteredFields
            .Where(fiel => fiel.FieldType.IsGenericType && fiel.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            .Select(fiel => fiel.Name)
            .ToArray();

        if (listFieldNames.Length > 0)
        {
            if (listFieldNames.Length > 0)
            {
                int selectedIndex = Array.IndexOf(listFieldNames, viewtarget.SetectedValue);
                if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex = EditorGUILayout.Popup("SLotList", selectedIndex, listFieldNames);

                viewtarget.SetectedValue = listFieldNames[selectedIndex];

                FieldInfo selectField = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(f => f.Name == viewtarget.SetectedValue);

                Type fieldtype = selectField.FieldType;

                Type elementType = fieldtype.GetGenericArguments()[0];

                FieldInfo[] listField = elementType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                FieldInfo[] gameObjectFields = listField
                           .Where(field => field.FieldType == typeof(GameObject))
                           .ToArray();

                string[] spriteFields = gameObjectFields.Select(listField => listField.Name).ToArray();

                if (spriteFields.Length > 0)
                {
                    int selectedIndex1 = Array.IndexOf(spriteFields, viewtarget.SelectedValue1);
                    if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                    //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                    selectedIndex1 = EditorGUILayout.Popup("ValueName", selectedIndex1, spriteFields);

                    viewtarget.SelectedValue1 = spriteFields[selectedIndex1];

                    viewtarget.viewController.SlotGameObjectSet(viewtarget.uITypeNumber, viewtarget.SetectedValue, viewtarget.SetectedValue1, viewtarget.gameObject, viewtarget.slotNumber);
                }

            }
        }
    }
    
    void SlotSliderType(Type selectedType)
    {
        viewtarget.slotNumber = EditorGUILayout.IntField("SlotNumber", viewtarget.slotNumber);

        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] filteredFields = field.Where(field => field.FieldType != typeof(Sprite) &&
            !typeof(Delegate).IsAssignableFrom(field.FieldType))
            .ToArray();

        string[] listFieldNames = filteredFields
            .Where(fiel => fiel.FieldType.IsGenericType && fiel.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            .Select(fiel => fiel.Name)
            .ToArray();

        if (listFieldNames.Length > 0)
        {

            int selectedIndex = Array.IndexOf(listFieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("SLotList", selectedIndex, listFieldNames);

            viewtarget.SetectedValue = listFieldNames[selectedIndex];

            FieldInfo selectField = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(f => f.Name == viewtarget.SetectedValue);

            Type fieldtype = selectField.FieldType;

            Type elementType = fieldtype.GetGenericArguments()[0];

            FieldInfo[] listfield = elementType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            string[] fieldName = listfield.Where(field =>
                field.FieldType == typeof(int) ||
                field.FieldType == typeof(float))
                .Select(field => field.Name)
               .ToArray();

            PropertyInfo[] property = elementType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string[] propertyName = property.Where(property =>
                property.PropertyType == typeof(int) ||
                property.PropertyType == typeof(float)).Select(pro => pro.Name)
            .ToArray();
            string[] combinedName = fieldName.Concat(propertyName).ToArray();

            string[] conbinedArray = fieldName.Concat(propertyName).ToArray();


            if (conbinedArray.Length > 0)
            {
                string[] fieldNames = new string[conbinedArray.Length];
                for (int i = 0; i < conbinedArray.Length; i++)
                {
                    fieldNames[i] = conbinedArray[i];

                }

                int selectedIndex1 = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
                if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex1 = EditorGUILayout.Popup("MaxValue", selectedIndex1, fieldNames);

                viewtarget.SetectedValue1 = fieldNames[selectedIndex1];

                object va = default;

                if (fieldName.Contains(viewtarget.SetectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Field;
                    va = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue1);
                }
                else if (propertyName.Contains(viewtarget.SetectedValue1))
                {
                    viewtarget.value1_type = UI.valueType.Property;
                    va = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue1);
                }

                viewtarget.value = va;
                viewtarget.valueText = va.ToString();
                viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);



                int selectedIndex2 = Array.IndexOf(fieldNames, viewtarget.SetectedValue2);
                if (selectedIndex2 == -1) selectedIndex2 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex2 = EditorGUILayout.Popup("CurrentValue", selectedIndex2, fieldNames);

                viewtarget.SetectedValue2 = fieldNames[selectedIndex2];

                object va2 = default;

                if (fieldName.Contains(viewtarget.SetectedValue2))
                {
                    viewtarget.value2_type = UI.valueType.Field;
                    va2 = viewtarget.viewController.GetValue(viewtarget.uITypeNumber, viewtarget.SetectedValue2);
                }
                else if (propertyName.Contains(viewtarget.SetectedValue2))
                {
                    viewtarget.value2_type = UI.valueType.Property;
                    va2 = viewtarget.viewController.GetPropertyValue(viewtarget.uITypeNumber, viewtarget.SetectedValue2);
                }

                viewtarget.value2 = va2;
                viewtarget.valueText2 = va.ToString();

                viewtarget.valueText2 = EditorGUILayout.TextField("Value2", viewtarget.valueText);


            }
        }
    }
}

        

