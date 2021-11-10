using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Отрисовка всего едитора для ObstacleSpaceMono.
/// Используется для рассчета общей массы и процентов для каждого чанка.
/// </summary>
[CustomEditor(typeof(ObstacleSpaceMono))]
public class ObstacleSpaceMonoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RecalculateChances();
    }

    private void RecalculateChances()
    {
        SerializedProperty chunksProperty = serializedObject.FindProperty(ObstacleSpaceMono.SelectableChunksFieldName);
        List<SerializedObject> chunkObjects = new List<SerializedObject>();
        int totalMass = GetTotalMass(chunksProperty, chunkObjects);
        RecalculateChancePercent(chunkObjects, totalMass);
    }

    private int GetTotalMass(SerializedProperty chunksProperty, List<SerializedObject> chunkObjects)
    {
        int arraySize = chunksProperty.arraySize;
        int totalMass = 0;
        for (int i = 0; i < arraySize; i++)
        {
            Object referenceObject = chunksProperty.GetArrayElementAtIndex(i).objectReferenceValue;
            if (referenceObject == null)
            {
                continue;
            }
            SerializedObject propertyObject = new SerializedObject(referenceObject);
            chunkObjects.Add(propertyObject);
            int chunkMass = propertyObject.FindProperty(SelectableChunk.ChanceMassFieldName).intValue;
            totalMass += chunkMass;
        }
        return totalMass;
    }

    private void RecalculateChancePercent(List<SerializedObject> chunkObjects, int totalMass)
    {
        chunkObjects.ForEach(chunkObject =>
        {
            float chancePercent = (float)chunkObject.FindProperty(SelectableChunk.ChanceMassFieldName).intValue / totalMass;
            chunkObject.FindProperty(SelectableChunk.ChancePercentFieldName).floatValue = chancePercent * 100f;
            chunkObject.ApplyModifiedProperties();
        });
    }
}
