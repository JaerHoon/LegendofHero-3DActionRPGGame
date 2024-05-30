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
    // ViewModel의 자식 클래스의 타입을 모두 담을 배열
    private Type[] derivedtype;
    UI viewtarget;

    private void OnEnable()
    {
        // ViewModel을 상속받는 모든 클래스를 찾아서 배열로 저장
        derivedtype = Assembly.GetAssembly(typeof(UIModel)).GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(UIModel)))
            .ToArray();
    }

    public override void OnInspectorGUI()
    {
        viewtarget = (UI)target; // 에디터를 적용할 대상을 정한다.

        viewtarget.viewController = (UIController)EditorGUILayout.ObjectField("UIController", viewtarget.viewController, typeof(UIController), true);

        //ViewController.UIType 이넘을 인스팩터에 표시해라."UI_Type" 라는 이름으로...
        viewtarget.uIType = (UIController.UIType)EditorGUILayout.EnumPopup("UIModelName", viewtarget.uIType);

        viewtarget.viewType = (UI.ViewType)EditorGUILayout.EnumPopup("UIType", viewtarget.viewType);

        //viewtarget.uIType에서 선택된 이넘 값과 이름을 같은 클래스를 찾아서 Type를 저장
        Type selectedtype = derivedtype.FirstOrDefault(t => t.Name == viewtarget.uIType.ToString());

        

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


            }

        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(viewtarget);
        }

    }

    void ImageType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] spriteFields = field.Where(field => field.FieldType == typeof(Sprite)).ToArray();

        if (spriteFields.Length > 0)
        {
            string[] fieldNames = new string[spriteFields.Length];
            for (int i = 0; i < spriteFields.Length; i++)
            {
                fieldNames[i] = spriteFields[i].Name;
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("SpritName", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = viewtarget.viewController.GetValue(viewtarget.uIType, viewtarget.SetectedValue);
            viewtarget.value = va;
            Sprite sp = (Sprite)va;
            viewtarget.valueText = sp.name;

            viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);
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
            string[] spriteFields = listField
                                      .Where(listField => listField.FieldType == typeof(Sprite))
                                      .Select(listField => listField.Name)
                                      .ToArray();

            if (spriteFields.Length > 0)
            {
                int selectedIndex1 = Array.IndexOf(spriteFields, viewtarget.SelectedValue1);
                if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex1 = EditorGUILayout.Popup("SpritName", selectedIndex1, spriteFields);

                viewtarget.SelectedValue1 = spriteFields[selectedIndex1];

                object obj = viewtarget.viewController.GetSlotValue(viewtarget.uIType, viewtarget.SetectedValue, viewtarget.SelectedValue1, viewtarget.slotNumber);

                viewtarget.value = obj;

                Sprite sp = (Sprite)obj;
                viewtarget.valueText = sp.name;

                viewtarget.valueText = EditorGUILayout.TextField("SpriteName", viewtarget.valueText);

            }


        }

    }

    void TextType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] nonSpriteFields = field.Where(field => field.FieldType != typeof(Sprite)
                                       && !(field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
                                       .ToArray();

        if (nonSpriteFields.Length > 0)
        {
            string[] fieldNames = new string[nonSpriteFields.Length];
            for (int i = 0; i < nonSpriteFields.Length; i++)
            {
                fieldNames[i] = nonSpriteFields[i].Name;
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("ValueName", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = viewtarget.viewController.GetValue(viewtarget.uIType, viewtarget.SetectedValue);
            viewtarget.value = va;
            viewtarget.valueText = va.ToString();

            viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);
        }

    }

    void TextSlotType(Type selectedType)
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
            FieldInfo[] nonSpriteFields = field.Where(field => field.FieldType != typeof(Sprite)
                                       && !(field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
                                       .ToArray();

            string[] spriteFields = nonSpriteFields.Select(listField => listField.Name).ToArray();

            if (spriteFields.Length > 0)
            {
                int selectedIndex1 = Array.IndexOf(spriteFields, viewtarget.SelectedValue1);
                if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

                //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
                selectedIndex1 = EditorGUILayout.Popup("ValueName", selectedIndex1, spriteFields);

                viewtarget.SelectedValue1 = spriteFields[selectedIndex1];



                object obj = viewtarget.viewController.GetSlotValue(viewtarget.uIType, viewtarget.SetectedValue, viewtarget.SelectedValue1, viewtarget.slotNumber);

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

            viewtarget.viewController.GetMethod(viewtarget.uIType, ref viewtarget.ButtonPressed, viewtarget.SetectedValue);


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

            viewtarget.viewController.GetSlotMethod(viewtarget.uIType, ref viewtarget.SlotButtonPressed, viewtarget.SetectedValue, viewtarget.slotNumber);
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

            viewtarget.viewController.GetMethod(viewtarget.uIType, ref viewtarget.Ondrag, viewtarget.SetectedValue);

            int selectedIndex1 = Array.IndexOf(MethodNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("DraggingMethod", selectedIndex1, MethodNames);

            viewtarget.SetectedValue1 = MethodNames[selectedIndex1];

            viewtarget.Dragging = null;

            viewtarget.viewController.GetMethod(viewtarget.uIType, ref viewtarget.Dragging, viewtarget.SetectedValue1);

            int selectedIndex2 = Array.IndexOf(MethodNames, viewtarget.SetectedValue2);
            if (selectedIndex2 == -1) selectedIndex2 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex2 = EditorGUILayout.Popup("EndDragMethod", selectedIndex2, MethodNames);

            viewtarget.SetectedValue2 = MethodNames[selectedIndex2];

            viewtarget.OffDrag = null;

            viewtarget.viewController.GetMethod(viewtarget.uIType, ref viewtarget.OffDrag, viewtarget.SetectedValue2);

            int selectedIndex3 = Array.IndexOf(MethodNames, viewtarget.SetectedValue3);
            if (selectedIndex3 == -1) selectedIndex3 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex3 = EditorGUILayout.Popup("DropMethod", selectedIndex3, MethodNames);

            viewtarget.SetectedValue3 = MethodNames[selectedIndex3];

            viewtarget.Drop = null;

            viewtarget.viewController.GetMethod(viewtarget.uIType, ref viewtarget.Drop, viewtarget.SetectedValue3);


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

            viewtarget.viewController.GetSlotMethod(viewtarget.uIType, ref viewtarget.SlotOndrag, viewtarget.SetectedValue, viewtarget.slotNumber);

            int selectedIndex1 = Array.IndexOf(MethodNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("DraggingMethod", selectedIndex1, MethodNames);

            viewtarget.SetectedValue1 = MethodNames[selectedIndex1];

            viewtarget.SlotDragging = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uIType, ref viewtarget.SlotDragging, viewtarget.SetectedValue1, viewtarget.slotNumber);

            int selectedIndex2 = Array.IndexOf(MethodNames, viewtarget.SetectedValue2);
            if (selectedIndex2 == -1) selectedIndex2 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex2 = EditorGUILayout.Popup("EndDragMethod", selectedIndex2, MethodNames);

            viewtarget.SetectedValue2 = MethodNames[selectedIndex2];

            viewtarget.SlotOffDarg = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uIType, ref viewtarget.SlotOffDarg, viewtarget.SetectedValue2, viewtarget.slotNumber);

            int selectedIndex3 = Array.IndexOf(MethodNames, viewtarget.SetectedValue3);
            if (selectedIndex3 == -1) selectedIndex3 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex3 = EditorGUILayout.Popup("DropMethod", selectedIndex3, MethodNames);

            viewtarget.SetectedValue3 = MethodNames[selectedIndex3];

            viewtarget.SlotDrop = null;

            viewtarget.viewController.GetSlotMethod(viewtarget.uIType, ref viewtarget.SlotDrop, viewtarget.SetectedValue3, viewtarget.slotNumber);

        }



    }

    void SliderType(Type selectedType)
    {
        FieldInfo[] field = selectedType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        FieldInfo[] nonSpriteFields = field.Where(field => field.FieldType != typeof(Sprite)
                                       && !(field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
                                       .ToArray();

        if (nonSpriteFields.Length > 0)
        {
            string[] fieldNames = new string[nonSpriteFields.Length];
            for (int i = 0; i < nonSpriteFields.Length; i++)
            {
                fieldNames[i] = nonSpriteFields[i].Name;
            }

            int selectedIndex = Array.IndexOf(fieldNames, viewtarget.SetectedValue);
            if (selectedIndex == -1) selectedIndex = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex = EditorGUILayout.Popup("MaxValue", selectedIndex, fieldNames);

            viewtarget.SetectedValue = fieldNames[selectedIndex];

            object va = viewtarget.viewController.GetValue(viewtarget.uIType, viewtarget.SetectedValue);
            viewtarget.value = va;
            viewtarget.valueText = va.ToString();

            viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);

            int selectedIndex1 = Array.IndexOf(fieldNames, viewtarget.SetectedValue1);
            if (selectedIndex1 == -1) selectedIndex1 = 0; //만약 선택된 값이 없다면 인덱스 번호 초기화

            //선택된 이넘 값을 가지고 "ValueName"값을 가지고 인스팩터 창에  나타낸다.
            selectedIndex1 = EditorGUILayout.Popup("CurrentValue", selectedIndex1, fieldNames);

            viewtarget.SetectedValue1 = fieldNames[selectedIndex1];

            object va2 = viewtarget.viewController.GetValue(viewtarget.uIType, viewtarget.SetectedValue1);
            viewtarget.value2 = va2;
            viewtarget.valueText = va.ToString();

            viewtarget.valueText = EditorGUILayout.TextField("Value", viewtarget.valueText);
        }
    }


}

