using System.Linq;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using Desdiene.Types.Ranges.Positive;
using UnityEngine;
using Zenject;

public class ObstacleSpaceMono : MonoBehaviourExt
{
    [SerializeField] private SelectableChunk[] _selectableChunks;
    [SerializeField] private float _startPoint = 40f;
    [SerializeField] private float _offsetGeneration = 30f; // ������ �� ��������� ������� �����������, ������� ��������� ����� ��������
    [SerializeField] private FloatRange _safeSpaceBetweenChunks = new FloatRange(5f, 10f);

    private IUpdate _update;
    private ObstacleSpace _obstacleSpace; // ���� ��������, ����� �����������

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        ISelectableItem<Chunk>[] selectableChunks = _selectableChunks
            .Select(it => it as ISelectableItem<Chunk>)
            .ToArray();
        ObstaclesGenerationData obstaclesGenerationData = new ObstaclesGenerationData(selectableChunks,
                                                                                      _safeSpaceBetweenChunks,
                                                                                      _offsetGeneration);
        ObstacleSpaceData obstacleSpaceData = new ObstacleSpaceData(_startPoint,
                                                                    obstaclesGenerationData);
        ObstacleSpace obstacleSpace = new ObstacleSpace(this,
                                                        obstacleSpaceData,
                                                        componentsProxy.VisibleGameSpace);

        _obstacleSpace = obstacleSpace;
        _update = obstacleSpace;
    }

    private void Update()
    {
        _update.Invoke(Time.deltaTime);
    }

}
