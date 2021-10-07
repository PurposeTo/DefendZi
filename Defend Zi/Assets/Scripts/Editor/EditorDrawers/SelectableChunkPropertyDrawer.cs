using Desdiene.Types.Percents;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectableChunk))]
public class SelectableChunkPropertyDrawer : PropertyDrawer
{
    private static readonly Percent ChunkFieldPercentageSize = new Percent(0.8f);
    private static readonly Percent ChanceFieldPercentageSize = new Percent(0.19f);

    private float _rectPosX;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        _rectPosX = position.x;
        Rect chunkRect = CreatePercentageRect(position, ChunkFieldPercentageSize);
        Rect chanceMassRect = CreatePercentageRect(position, ChanceFieldPercentageSize);

        float massPercent = 0f;
        if (property.objectReferenceValue != null)
        {
            SerializedObject propertyObject = new SerializedObject(property.objectReferenceValue);
            SerializedProperty chanceMassProperty = propertyObject.FindProperty(SelectableChunk.ChanceMassFieldName);
            int mass = chanceMassProperty.intValue;
            massPercent = mass * 100f / SelectableChunksDrawable.TotalMass;
        }

        EditorGUI.PropertyField(chunkRect, property, GUIContent.none);
        GUI.enabled = false;
        EditorGUI.LabelField(chanceMassRect, $"{massPercent.ToString("0.00")} %", GUIStyle.none);
        GUI.enabled = true;

        EditorGUI.indentLevel = indent;
    }

    private Rect CreatePercentageRect(Rect position, Percent percentage)
    {
        float currPosX = _rectPosX;
        float currWidth = percentage * position.width;
        _rectPosX += currWidth + 5f;
        return new Rect(currPosX,
                    position.y,
                    percentage * position.width,
                    position.height);
    }
}
