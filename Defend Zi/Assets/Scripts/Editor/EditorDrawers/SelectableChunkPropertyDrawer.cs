using Desdiene.Types.Percents;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Этот дровер отвечает за отрисовку одного поля SelectableChunk
/// </summary>
[CustomPropertyDrawer(typeof(SelectableChunk))]
public class SelectableChunkPropertyDrawer : PropertyDrawer
{
    private static readonly IPercentAccessor _chunkFieldPercentageSize = new Percent(0.8f);
    private static readonly IPercentAccessor _chanceFieldPercentageSize = new Percent(0.19f);

    private float _rectPosX;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        _rectPosX = position.x;
        Rect chunkRect = GetFieldRect(position, _chunkFieldPercentageSize);
        Rect chanceMassRect = GetFieldRect(position, _chanceFieldPercentageSize);

        float massPercent = 0f;
        if (property.objectReferenceValue != null)
        {
            SerializedObject propertyObject = new SerializedObject(property.objectReferenceValue);
            SerializedProperty chancePercentProperty = propertyObject.FindProperty(SelectableChunk.ChancePercentFieldName);
            massPercent = chancePercentProperty == null ? 0f : chancePercentProperty.floatValue;
        }

        EditorGUI.PropertyField(chunkRect, property, GUIContent.none);
        GUI.enabled = false;
        EditorGUI.LabelField(chanceMassRect, $"{massPercent.ToString("0.00")} %", GUIStyle.none);
        GUI.enabled = true;

        EditorGUI.indentLevel = indent;
    }

    private Rect GetFieldRect(Rect position, IPercentAccessor percentage)
    {
        float currPosX = _rectPosX;
        float currWidth = percentage.Value * position.width;
        _rectPosX += currWidth + 5f;
        return new Rect(currPosX,
                    position.y,
                    percentage.Value * position.width,
                    position.height);
    }
}
