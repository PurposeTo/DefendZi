using Desdiene.Types.Percents;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectableChunk))]
public class SelectableChunkPropertyDrawer : PropertyDrawer
{
    public  static int TotalMass;
    private static readonly Percent ChunkRectPercentage = new Percent(0.8f);
    private static readonly Percent ChanceRectPercentage = new Percent(0.19f);

    private float rectPosX;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue == null)
        {
            return;
        }

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        rectPosX = position.x;
        Rect chunkRect = CreatePercentageRect(position, ChunkRectPercentage);
        Rect chanceMassRect = CreatePercentageRect(position, ChanceRectPercentage);

        SerializedObject propertyObject = new SerializedObject(property.objectReferenceValue);
        SerializedProperty chanceMassProperty = propertyObject.FindProperty(SelectableChunk.ChanceMassFieldName);
        int mass = chanceMassProperty.intValue;
        float massPercent = mass * 100f / TotalMass;

        EditorGUI.PropertyField(chunkRect, property, GUIContent.none);
        GUI.enabled = false;
        EditorGUI.LabelField(chanceMassRect, $"{massPercent.ToString("0.00")} %", GUIStyle.none);
        GUI.enabled = true;

        EditorGUI.indentLevel = indent;
    }

    private Rect CreatePercentageRect(Rect position, Percent percentage)
    {
        float currPosX = rectPosX;
        float currWidth = percentage * position.width;
        rectPosX += currWidth + 5f;
        return new Rect(currPosX,
                    position.y,
                    percentage * position.width,
                    position.height);
    }
}
