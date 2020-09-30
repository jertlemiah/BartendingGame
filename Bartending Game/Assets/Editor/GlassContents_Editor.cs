using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlassContents))]
//[CanEditMultipleObjects]
public class GlassContents_Editor : Editor
{
    public int sliderVolume_Max = 10;
    public int sliderVolume_min = 0;

    public int sliderColor_Max = 255;
    public int sliderColor_min = 0;

    float m_Red, m_Blue, m_Green, m_Alpha;

    public bool showColorSliders = false;

    private int ListSize = 0;

    private const float newLiquidButtonWidth = 125f;

    //public class GlassContents_Editor : EditorWindow
    //[MenuItem("Examples/Editor GUILayout Slider usage")]
    //static void Init()
    //{
    //    EditorWindow window = GetWindow(typeof(GlassContents_Editor));
    //    window.Show();
    //}

    //void OnGUI()
    //{
    //    scale = EditorGUILayout.Slider(scale, slider_min, slider_Max);
    //}
    // Update is called once per frame
    //void OnInsepctorUpdate()
    //{
    //    if (Selection.activeTransform)
    //        Selection.activeTransform.localScale = new Vector3(scale, scale, scale);
    //}

    private SerializedProperty currentVolumeLayer;
    private SerializedProperty LiquidsList;
    //SerializedProperty drinkDisplayObj;

    void OnEnable()
    {
        // Cache the target reference
        GlassContents myGlassContets = (GlassContents)target;

        // Cache the SerializedProperties
        currentVolumeLayer = serializedObject.FindProperty("currentVolumeLayer");
        

        sliderVolume_Max = myGlassContets.spriteList.Length - 1;
        LiquidsList = serializedObject.FindProperty("LiquidsList");
        //slider_Max = serializedObject.FindProperty("maxVolume");
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // must be first line
        DrawDefaultInspector();
        GlassContents myGlassContets = (GlassContents)target;

        //_____________ Object Info ____________________
        EditorGUILayout.LabelField("Object Info", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
            myGlassContets.glassType = (GlassType)EditorGUILayout.ObjectField("Glass Obj", myGlassContets.glassType, typeof(GlassType), true);
            //EditorGUILayout.TextField("Glass Type", myGlassContets.glassType);
            if(myGlassContets.glassType != null)
            {
                EditorGUI.BeginDisabledGroup(true); // makes value ready only
                EditorGUILayout.DoubleField("Max Volume", myGlassContets.glassType.maxVolume);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
            EditorGUILayout.LabelField("Can't display Max Volume without Glass Obj");
            }

            myGlassContets.currentVolume = EditorGUILayout.DoubleField("Cur Volume", myGlassContets.currentVolume);
        EditorGUI.indentLevel--;

        //_____________ Liquid Info ____________________
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Liquid Info", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        SerializedProperty LiquidsList = serializedObject.FindProperty("LiquidsList");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(LiquidsList, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        //// Create a right-aligned button which when clicked, creates a new Liquid in the Liquids array.

        
        ////EditorGUI.

        ////Resize our list
        //EditorGUILayout.Space();
        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("Define the list size with a number");
        //ListSize = LiquidsList.arraySize;
        //ListSize = EditorGUILayout.IntField("List Size", ListSize);

        //if (ListSize != LiquidsList.arraySize)
        //{
        //    while (ListSize > LiquidsList.arraySize)
        //    {
        //        LiquidsList.InsertArrayElementAtIndex(LiquidsList.arraySize);
        //    }
        //    while (ListSize < LiquidsList.arraySize)
        //    {
        //        LiquidsList.DeleteArrayElementAtIndex(LiquidsList.arraySize - 1);
        //    }
        //}

        ////EditorGUILayout.Space();
        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("Or");
        //EditorGUILayout.Space();
        ////EditorGUILayout.Space();

        ////Or add a new item to the List<> with a button
        //EditorGUILayout.LabelField("Add a new item with a button");

        //if (GUILayout.Button("Add New"))
        //{
        //    //myGlassContets.LiquidsList.Add(new CustomList.Liquid());
        //    myGlassContets.LiquidsList.Add(new Liquid());
        //}

        //EditorGUILayout.Space();
        //EditorGUILayout.Space();

        //EditorGUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        ////foreach (Liquid liquid in myGlassContets.liquids)
        //for (int i = 0; i < myGlassContets.LiquidsList.Count; i++)
        //{
        //    GUILayout.BeginHorizontal(EditorStyles.helpBox);
        //    SerializedProperty MyListRef = LiquidsList.GetArrayElementAtIndex(i);
        //    if (myGlassContets.LiquidsList[i] != null)
        //    {
        //        //Editor.CreateEditor(myGlassContets.liquids[i]);
        //        SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
        //        SerializedProperty liquidType = MyListRef.FindPropertyRelative("liquidType");
        //        //SerializedProperty liquidColor = liquidType.FindPropertyRelative("liquidColor");

        //        liquidType.objectReferenceValue = EditorGUILayout.ObjectField("Liquid Type", liquidType.objectReferenceValue, typeof(LiquidType), true);
        //        //liquidColor.colorValue = EditorGUILayout.ColorField(liquidColor.colorValue);
        //        //EditorGUILayout.ColorField(liquidType.liquidColor);
        //        volume.doubleValue = EditorGUILayout.DoubleField("Liquid Volume", volume.doubleValue);
        //    }
        //    EditorGUILayout.EndHorizontal();
        //    EditorGUILayout.Space();

        //    //Remove this index from the List
        //    EditorGUILayout.LabelField("Remove an index from the List<> with a button");
        //    if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
        //    {
        //        LiquidsList.DeleteArrayElementAtIndex(i);
        //    }
        //EditorGUILayout.Space();
        //    EditorGUILayout.Space();
        //    EditorGUILayout.Space();
        ////}
        //if (GUILayout.Button("Add Liquid", GUILayout.Width(newLiquidButtonWidth)))
        //{
        //    //ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection();
        //    //collectionsProperty.AddToObjectArray(newCollection);
        //    //myGlassContets.liquids. ;
        //    //liquidsProperty.AddToObjectArray();
        //}
        //EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;

        //_____________ Animation Options ____________________
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Options", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
            myGlassContets.m_SpriteRenderer = (SpriteRenderer)EditorGUILayout.ObjectField("Sprite Renderer", myGlassContets.m_SpriteRenderer, typeof(SpriteRenderer), true);       
            myGlassContets.currentVolumeLayer = EditorGUILayout.IntSlider(
                "Cur Vol Layer: ", myGlassContets.currentVolumeLayer, sliderVolume_min, sliderVolume_Max);
        
            //EditorGUILayout.
            //EditorGUIUtility.LookLikeInspector();
            SerializedProperty spriteList = serializedObject.FindProperty("spriteList");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(spriteList, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        EditorGUI.indentLevel--;

        //_____________ Color Options ____________________
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Color Options", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(true); // makes value ready only
            EditorGUILayout.ColorField("Mixed Color: ", myGlassContets.Color_Mixed);
            EditorGUI.EndDisabledGroup();

            myGlassContets.OverrideColor = EditorGUILayout.Toggle("Override Color?", myGlassContets.OverrideColor);
            if (myGlassContets.OverrideColor)
            {
                EditorGUI.BeginDisabledGroup(true); // makes value ready only
                EditorGUILayout.ColorField("New Color: ", myGlassContets.Color_Override);
                EditorGUI.EndDisabledGroup();

                //Use the Slider to change amount of red in the Color
                m_Red = EditorGUILayout.Slider("Red: ", m_Red, 0, sliderColor_Max);

                //The Slider manipulates the amount of green in the GameObject
                m_Green = EditorGUILayout.Slider("Green: ", m_Green, 0, sliderColor_Max);

                //This Slider decides the amount of blue in the GameObject
                m_Blue = EditorGUILayout.Slider("Blue: ", m_Blue, 0, sliderColor_Max);

                //This Slider decides the amount of blue in the GameObject
                m_Alpha = EditorGUILayout.Slider("Alpha: ", m_Alpha, 0, 1);

            //Set the Color to the values gained from the Sliders
            myGlassContets.Color_Override = new
                    Color(m_Red / sliderColor_Max, m_Green / sliderColor_Max, m_Blue / sliderColor_Max, m_Alpha);
            }
        EditorGUI.indentLevel--;

        //myGlassContets.currentVolume = EditorGUILayout.IntField("currentVolume", myGlassContets.currentVolume);
        //EditorGUILayout.LabelField("Curent Volume");
        //EditorGUILayout.PropertyField(currentVolume);
        //EditorGUILayout.Slider(scale, 0, slider_Max);

        //EditorGUILayout.Space();

        //EditorGUILayout.PropertyField(drinkDisplayObj);

        // apply changes at end
        serializedObject.ApplyModifiedProperties();
    }
}
