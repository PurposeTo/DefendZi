using Desdiene.MonoBehaviourExtension;
using Desdiene.Randoms;
using Desdiene.Types.Ranges.Positive;
using Desdiene.Types.Rectangles;
using UnityEngine;
using Zenject;

public class ObstacleSpaceMono : MonoBehaviourExt
{
    #region Properties for editor drawer

    public static string SelectableChunksFieldName => nameof(_selectableChunks);

    #endregion

    [SerializeField] private SelectableChunk[] _selectableChunks;
    [SerializeField] private float _startPoint = 20f;
    [SerializeField] private float _offsetGeneration = 30f; // Сейчас не учитывает размеры препятствий, поэтому поставить число побольше
    [SerializeField] private FloatRange _safeSpaceBetweenChunks = new FloatRange(5f, 10f);

    private ObstacleSpace _obstacleSpace; // пока оставить, может пригодиться
    private IRectangleIn2DAccessor _visibleGameSpace;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _visibleGameSpace = componentsProxy.VisibleGameSpace;
    }

    protected override void AwakeExt()
    {
        ObstaclesGenerationData obstaclesGenerationData = new ObstaclesGenerationData(new SelectableItems<Chunk>(_selectableChunks),
                                                                                      _safeSpaceBetweenChunks,
                                                                                      _offsetGeneration);
        ObstacleSpaceData obstacleSpaceData = new ObstacleSpaceData(_startPoint,
                                                                    obstaclesGenerationData);
        ObstacleSpace obstacleSpace = new ObstacleSpace(this,
                                                        obstacleSpaceData,
                                                        _visibleGameSpace);

        _obstacleSpace = obstacleSpace;
    }
}
