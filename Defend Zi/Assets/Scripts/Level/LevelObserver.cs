using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// ������ ����� ������ �� �������������� ������ � �������� �������, ����� ���������� ������������� ����� ����� ������.
/// </summary>
// todo: ������ �� ������ ����� ���������� ���������� � ��� ������� ������, ������� ��� �� ������������, �.�. ������� ����� �������?
public class LevelObserver
{
    private IPositionGetter _playerPosition; //������������ ����� ����� �� ���� ����������� ������
    private CameraSize _cameraSize; //����� ����� ������������ ��� ���� ���������

    public event Action OnNeedToGenerage;
}
