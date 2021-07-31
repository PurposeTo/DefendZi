using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Range.Positive;
using UnityEngine;

public class ObstacleSpaceMono : MonoBehaviourExt
{
    [SerializeField] private SelectableChunk[] _selectableChunks;
    [SerializeField] private float _startPoint = 40f;
    [SerializeField] private FloatRange _extraSpaceGeneration = new FloatRange(5f, 10f);

    private ObstacleSpace _obstacleSpace;

    protected override void AwakeExt()
    {
        IRandomlySelectableItem<Chunk>[] selectableChunks = _selectableChunks
            .Select(it => it as IRandomlySelectableItem<Chunk>)
            .ToArray();
        ObstaclesGenerationData obstaclesGenerationData = new ObstaclesGenerationData(selectableChunks, _extraSpaceGeneration);
        ObstacleSpaceData obstacleSpaceData = new ObstacleSpaceData(_startPoint, obstaclesGenerationData);
        _obstacleSpace = new ObstacleSpace(this, obstacleSpaceData);
    }
}
