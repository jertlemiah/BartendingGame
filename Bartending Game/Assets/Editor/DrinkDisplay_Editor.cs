using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(drinkDisplay))]
public class DrinkDisplay_Editor : Editor
{
    SerializedProperty NewColor;
    public int slider_Max = 255;
    public int slider_min = 0;

    float m_Red, m_Blue, m_Green;

    void OnEnable()
    {
        NewColor = serializedObject.FindProperty("NewColor");
        drinkDisplay myDrinkDisplay = (drinkDisplay)target;
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // must be first line

        DrawDefaultInspector();
        drinkDisplay myDrinkDisplay = (drinkDisplay)target;

        //Use the Slider to change amount of red in the Color
        m_Red = EditorGUILayout.Slider("Red: ", m_Red, 0, slider_Max);

        //The Slider manipulates the amount of green in the GameObject
        m_Green = EditorGUILayout.Slider("Green: ", m_Green, 0, slider_Max);

        //This Slider decides the amount of blue in the GameObject
        m_Blue = EditorGUILayout.Slider("Blue: ", m_Blue, 0, slider_Max);

        //Set the Color to the values gained from the Sliders
        myDrinkDisplay.Color_Override = new Color(m_Red/ slider_Max, m_Green/ slider_Max, m_Blue/ slider_Max);


        // apply changes at end
        serializedObject.ApplyModifiedProperties();
    }
}
