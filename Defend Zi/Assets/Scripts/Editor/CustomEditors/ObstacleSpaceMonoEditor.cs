using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleSpaceMono))]
public class ObstacleSpaceMonoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RecalculateTotalMass();
    }

    private void RecalculateTotalMass()
    {
        SerializedProperty chunksProperty = serializedObject.FindProperty("_selectableChunks");
        int arraySize = chunksProperty.arraySize;
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
    }
}
