using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class Chunk : MonoBehaviourExt
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public float Width => _width;
    public float Height => _height;

    // TODO: необходима визуализация размеров чанка
}
