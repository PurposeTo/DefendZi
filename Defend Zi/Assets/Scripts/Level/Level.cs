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
    //todo ������� ������� � ��������� �����.
    #region ������� ������.
    public static float BottomPoint => YAxisStart - (Height / 2f); // ������ ����� ������.
    public static float TopPoint => YAxisStart + (Height / 2f); // ������� ����� ������.
    public const float Height = 15; //������ ������ (��� ����� �������. ������ ������������� �����).
    public const float YAxisStart = 0f; // ������ Y ��� ������.
    public const float XAxisStart = 0f; // ������ X ��� ������.
    public const float ZAxisStart = 0f; // ������ Z ��� ������.
    #endregion

    private float _width = 0f;

    float ILevelSize.Height => Height;

    float ILevelSize.Width => _width;
}
