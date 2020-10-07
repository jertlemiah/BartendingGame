using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlassContentsNew))]
//[CanEditMultipleObjects]
public class GlassContentsNewEditor : Editor
{
    public int sliderVolume_Max = 10;
    public int sliderVolume_min = 0;

    public int sliderColor_Max = 255;
    public int sliderColor_min = 0;

    float m_Red, m_Blue, m_Green, m_Alpha;

    public bool showColorSliders = false;

    private int ListSize = 0;

    private const float newLiquidButtonWidth = 125f;

    private SerializedProperty currentVolumeLayer;
    private SerializedProperty LiquidsList;
    //SerializedProperty drinkDisplayObj;

    void OnEnable()
    {
        // Cache the target reference
        GlassContentsNew myGlassContetsNew = (GlassContentsNew)target;

        // Cache the SerializedProperties
        currentVolumeLayer = serializedObject.FindProperty("currentVolumeLayer");


        sliderVolume_Max = myGlassContetsNew.spriteList.Length - 1;
        //LiquidsList = serializedObject.FindProperty("LiquidsList");
        //slider_Max = serializedObject.FindProperty("maxVolume");
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // must be first line
        DrawDefaultInspector();
        GlassContentsNew myGlassContetsNew = (GlassContentsNew)target;

        ////_____________ Object Info ____________________
        //EditorGUILayout.LabelField("Object Info", EditorStyles.boldLabel);
        //EditorGUI.indentLevel++;
        //myGlassContets.glassType = (GlassType)EditorGUILayout.ObjectField("Glass Obj", myGlassContets.glassType, typeof(GlassType), true);
        ////EditorGUILayout.TextField("Glass Type", myGlassContets.glassType);
        //if (myGlassContets.glassType != null)
        //{
        //    EditorGUI.BeginDisabledGroup(true); // makes value ready only
        //    EditorGUILayout.DoubleField("Max Volume", myGlassContets.glassType.maxVolume);
        //    EditorGUI.EndDisabledGroup();
        //}
        //else
        //{
        //    EditorGUILayout.LabelField("Can't display Max Volume without Glass Obj");
        //}

        //myGlassContets.currentVolume = EditorGUILayout.DoubleField("Cur Volume", myGlassContets.currentVolume);
        //EditorGUI.indentLevel--;

        ////_____________ Liquid Info ____________________
        //EditorGUILayout.Space();
        //EditorGUILayout.Space();

        //EditorGUILayout.LabelField("Liquid Info", EditorStyles.boldLabel);
        //EditorGUI.indentLevel++;

        //SerializedProperty LiquidsList = serializedObject.FindProperty("LiquidsList");
        //EditorGUI.BeginChangeCheck();
        //EditorGUILayout.PropertyField(LiquidsList, true);
        //if (EditorGUI.EndChangeCheck())
        //    serializedObject.ApplyModifiedProperties();


        //EditorGUI.indentLevel--;

        ////_____________ Animation Options ____________________
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animation Options", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        myGlassContetsNew.rendererList[0] = 
            (SpriteRenderer)EditorGUILayout.
            ObjectField("Sprite Renderer", myGlassContetsNew.rendererList[0], 
            typeof(SpriteRenderer), true);
        myGlassContetsNew.currentVolumeLayer = EditorGUILayout.IntSlider(
            "Cur Vol Layer: ", myGlassContetsNew.currentVolumeLayer, sliderVolume_min, sliderVolume_Max);

        ////EditorGUILayout.
        ////EditorGUIUtility.LookLikeInspector();
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
        //EditorGUI.BeginDisabledGroup(true); // makes value ready only
        //EditorGUILayout.ColorField("Mixed Color: ", myGlassContets.Color_Mixed);
        //EditorGUI.EndDisabledGroup();

        myGlassContetsNew.OverrideColor = EditorGUILayout.Toggle("Override Color?", myGlassContetsNew.OverrideColor);
        if (myGlassContetsNew.OverrideColor)
        {
            EditorGUI.BeginDisabledGroup(true); // makes value ready only
            EditorGUILayout.ColorField("New Color: ", myGlassContetsNew.Color_Override);
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
            myGlassContetsNew.Color_Override = new
                    Color(m_Red / sliderColor_Max, 
                    m_Green / sliderColor_Max, 
                    m_Blue / sliderColor_Max,
                    m_Alpha / sliderColor_Max);
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
