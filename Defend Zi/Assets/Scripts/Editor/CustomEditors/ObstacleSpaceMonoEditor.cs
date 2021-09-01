using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleSpaceMono))]
public class ObstacleSpaceMonoEditor : Editor
{
    private int previousArraySize = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RecalculateTotalMass();
    }

    private void RecalculateTotalMass()
    {
        SerializedProperty chunksProperty = serializedObject.FindProperty(ObstacleSpaceMono.SelectableChunkFieldName);
        int arraySize = chunksProperty.arraySize;
        if (arraySize != previousArraySize)
        {
            SelectableChunkPropertyDrawer.TotalMass = 0;
            for (int i = 0; i < arraySize; i++)
            {
                Object referenceObject = chunksProperty.GetArrayElementAtIndex(i).objectReferenceValue;
                if (referenceObject == null)
                {
                    continue;
                }
                SerializedObject propertyObject = new SerializedObject(referenceObject);
                int chunkMass = propertyObject.FindProperty(SelectableChunk.ChanceMassFieldName).intValue;
                SelectableChunkPropertyDrawer.TotalMass += chunkMass;
            }
            previousArraySize = arraySize;
        }
    }
}
