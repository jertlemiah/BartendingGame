using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CustomList))]

public class CustomListEditor : Editor
{
    //https://forum.unity.com/threads/display-a-list-class-with-a-custom-editor-script.227847/

    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    CustomList t;
    SerializedObject GetTarget;
    SerializedProperty LiquidsList;
    int ListSize;

    void OnEnable()
    {
        t = (CustomList)target;
        GetTarget = new SerializedObject(t);
        LiquidsList = GetTarget.FindProperty("LiquidsList"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        //Update our list

        GetTarget.Update();
        DrawDefaultInspector();
        //Choose how to display the list<> Example purposes only
        //EditorGUILayout.Space();
        //EditorGUILayout.Space();
        //DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("", DisplayFieldType);

        //Resize our list
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Define the list size with a number");
        ListSize = LiquidsList.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != LiquidsList.arraySize)
        {
            while (ListSize > LiquidsList.arraySize)
            {
                LiquidsList.InsertArrayElementAtIndex(LiquidsList.arraySize);
            }
            while (ListSize < LiquidsList.arraySize)
            {
                LiquidsList.DeleteArrayElementAtIndex(LiquidsList.arraySize - 1);
            }
        }

        //EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Or");
        EditorGUILayout.Space();
        //EditorGUILayout.Space();

        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.LiquidsList.Add(new CustomList.Liquid2());
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < LiquidsList.arraySize; i++)
        {
            SerializedProperty MyListRef = LiquidsList.GetArrayElementAtIndex(i);

            SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
            SerializedProperty liquidType = MyListRef.FindPropertyRelative("liquidType");
            //SerializedProperty liquidColor = liquidType.FindPropertyRelative("liquidColor");

            liquidType.objectReferenceValue = EditorGUILayout.ObjectField("Liquid Type", liquidType.objectReferenceValue, typeof(LiquidType), true);
            //liquidColor.colorValue = EditorGUILayout.ColorField(liquidColor.colorValue);
            //EditorGUILayout.ColorField(liquidType.liquidColor);
            volume.doubleValue = EditorGUILayout.DoubleField("Liquid Volume", volume.doubleValue);


            //SerializedProperty MyInt = MyListRef.FindPropertyRelative("AnInt");
            //SerializedProperty MyFloat = MyListRef.FindPropertyRelative("AnFloat");
            //SerializedProperty MyVect3 = MyListRef.FindPropertyRelative("AnVector3");
            //SerializedProperty MyGO = MyListRef.FindPropertyRelative("AnGO");
            //SerializedProperty MyArray = MyListRef.FindPropertyRelative("AnIntArray");


            // Display the property fields in two ways.

            //if (DisplayFieldType == 0)
            //{// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
            //    //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
            //    EditorGUILayout.LabelField("Automatic Field By Property Type");
            //    EditorGUILayout.PropertyField(volume);
            //    EditorGUILayout.PropertyField(liquidType);

            //    //EditorGUILayout.PropertyField(MyGO);
            //    //EditorGUILayout.PropertyField(MyInt);
            //    //EditorGUILayout.PropertyField(MyFloat);
            //    //EditorGUILayout.PropertyField(MyVect3);

            //    // Array fields with remove at index
            //    EditorGUILayout.Space();
            //    EditorGUILayout.Space();
            //    EditorGUILayout.LabelField("Array Fields");

            //    if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
            //    {
            //        MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
            //        MyArray.GetArrayElementAtIndex(MyArray.arraySize - 1).intValue = 0;
            //    }

            //    for (int a = 0; a < MyArray.arraySize; a++)
            //    {
            //        EditorGUILayout.PropertyField(MyArray.GetArrayElementAtIndex(a));
            //        if (GUILayout.Button("Remove  (" + a.ToString() + ")", GUILayout.MaxWidth(100), GUILayout.MaxHeight(15)))
            //        {
            //            MyArray.DeleteArrayElementAtIndex(a);
            //        }
            //    }
            //}
            //else
            {
                //Or

                //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                //EditorGUILayout.LabelField("Customizable Field With GUI");    
                //MyGO.objectReferenceValue = EditorGUILayout.ObjectField("My Custom Go", MyGO.objectReferenceValue, typeof(GameObject), true);
                //MyInt.intValue = EditorGUILayout.IntField("My Custom Int", MyInt.intValue);
                //MyFloat.floatValue = EditorGUILayout.FloatField("My Custom Float", MyFloat.floatValue);
                //MyVect3.vector3Value = EditorGUILayout.Vector3Field("My Custom Vector 3", MyVect3.vector3Value);


                // Array fields with remove at index
                //EditorGUILayout.Space();
                //EditorGUILayout.Space();
                //EditorGUILayout.LabelField("Array Fields");

                //if (GUILayout.Button("Add New Index", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                //{
                //    MyArray.InsertArrayElementAtIndex(MyArray.arraySize);
                //    MyArray.GetArrayElementAtIndex(MyArray.arraySize - 1).intValue = 0;
                //}

                //for (int a = 0; a < MyArray.arraySize; a++)
                //{
                //    EditorGUILayout.BeginHorizontal();
                //    EditorGUILayout.LabelField("My Custom Int (" + a.ToString() + ")", GUILayout.MaxWidth(120));
                //    MyArray.GetArrayElementAtIndex(a).intValue = EditorGUILayout.IntField("", MyArray.GetArrayElementAtIndex(a).intValue, GUILayout.MaxWidth(100));
                //    if (GUILayout.Button("-", GUILayout.MaxWidth(15), GUILayout.MaxHeight(15)))
                //    {
                //        MyArray.DeleteArrayElementAtIndex(a);
                //    }
                //    EditorGUILayout.EndHorizontal();
                //}
            }

            EditorGUILayout.Space();

            //Remove this index from the List
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                LiquidsList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        //Apply the changes to our list
        GetTarget.ApplyModifiedProperties();
    }
}

//using UnityEditor;
//using UnityEditorInternal;
//using UnityEngine;

//[CustomEditor(typeof(TutorialShortcutData))]
////public class TutorialShortcutCustomEditor : Editor
//public class CustomListEditor : Editor
//{
//    private SerializedProperty m_shortcutData;
//    private ReorderableList m_ReorderableList;

//    private void OnEnable()
//    {
//        //Find the list in our ScriptableObject script.
//        m_shortcutData = serializedObject.FindProperty("shortcuts");

//        //Create an instance of our reorderable list.
//        m_ReorderableList = new ReorderableList(serializedObject: serializedObject, elements: m_shortcutData, draggable: true, displayHeader: true,
//            displayAddButton: true, displayRemoveButton: true);

//        //Set up the method callback to draw our list header
//        m_ReorderableList.drawHeaderCallback = DrawHeaderCallback;

//        //Set up the method callback to draw each element in our reorderable list
//        m_ReorderableList.drawElementCallback = DrawElementCallback;

//        //Set the height of each element.
//        m_ReorderableList.elementHeightCallback += ElementHeightCallback;

//        //Set up the method callback to define what should happen when we add a new object to our list.
//        m_ReorderableList.onAddCallback += OnAddCallback;
//    }

//    /// <summary>
//    /// Draws the header for the reorderable list
//    /// </summary>
//    /// <param name="rect"></param>
//    private void DrawHeaderCallback(Rect rect)
//    {
//        EditorGUI.LabelField(rect, "Shortcuts");
//    }

//    /// <summary>
//    /// This methods decides how to draw each element in the list
//    /// </summary>
//    /// <param name="rect"></param>
//    /// <param name="index"></param>
//    /// <param name="isactive"></param>
//    /// <param name="isfocused"></param>
//    private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
//    {
//        //Get the element we want to draw from the list.
//        SerializedProperty element = m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
//        rect.y += 2;

//        //We get the name property of our element so we can display this in our list.
//        SerializedProperty elementName = element.FindPropertyRelative("m_Name");
//        string elementTitle = string.IsNullOrEmpty(elementName.stringValue)
//          ? "New Shortcut"
//           : $"Shortcut: {elementName.stringValue}";

//        //Draw the list item as a property field, just like Unity does internally.
//        EditorGUI.PropertyField(position:
//          new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
//          element, label: new GUIContent(elementTitle), includeChildren: true);
//    }

//    /// <summary>
//    /// Calculates the height of a single element in the list.
//    /// This is extremely useful when displaying list-items with nested data.
//    /// </summary>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    private float ElementHeightCallback(int index)
//    {
//        //Gets the height of the element. This also accounts for properties that can be expanded, like structs.
//        float propertyHeight = EditorGUI.GetPropertyHeight(m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index), true);

//        float spacing = EditorGUIUtility.singleLineHeight / 2;

//        return propertyHeight + spacing;
//    }

//    /// <summary>
//    /// Defines how a new list element should be created and added to our list.
//    /// </summary>
//    /// <param name="list"></param>
//    private void OnAddCallback(ReorderableList list)
//    {
//        //Insert an extra item add the end of our list.
//        var index = list.serializedProperty.arraySize;
//        list.serializedProperty.arraySize++;
//        list.index = index;

//        //If we want to do anything with the item we just added,
//        //We can create reference by using this method
//        var element = list.serializedProperty.GetArrayElementAtIndex(index);
//    }

//    /// <summary>
//    /// Draw the Inspector Window
//    /// </summary>
//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        m_ReorderableList.DoLayoutList();

//        serializedObject.ApplyModifiedProperties();
//    }
//}