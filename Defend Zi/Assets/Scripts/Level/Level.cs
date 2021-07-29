using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// ��������� ������� ����.
/// ������� �� �����������.
/// </summary>
public class Level : ILevelSize
{
    private float _width = 0f;

    float ILevelSize.Height => LevelConfig.Height;

    float ILevelSize.Width => _width;
}
