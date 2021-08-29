using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectableChunk))]
public class SelectableChunkPropertyDrawer : PropertyDrawer
{
    private float rectPosX;
    private const int CHUNK_RECT_WIDTH = 80;
    private const int CHANCE_RECT_WIDTH = 20;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        rectPosX = position.x;
        Rect chunkRect = CreatePercentageRect(position, CHUNK_RECT_WIDTH);
        Rect chanceMassRect = CreatePercentageRect(position, CHANCE_RECT_WIDTH - 1);

        SerializedObject propertyObject = new SerializedObject(property.objectReferenceValue);
        SerializedProperty chanceMassProperty = propertyObject.FindProperty("_chanceMass");

        EditorGUI.PropertyField(chunkRect, property, GUIContent.none);
        GUI.enabled = false;
        EditorGUI.PropertyField(chanceMassRect, chanceMassProperty, GUIContent.none);
        GUI.enabled = true;

        EditorGUI.indentLevel = indent;
    }

    private Rect CreatePercentageRect(Rect position, byte percentage)
    {
        if (percentage > 100 || percentage < 0) throw new ArithmeticException();
        float currPosX = rectPosX;
        float currWidth = percentage / 100f * position.width;
        rectPosX += currWidth + 5f;
        return new Rect(currPosX, 
                    position.y, 
                    percentage / 100f * position.width, 
                    position.height);
    }
}
