using System;
using UnityEngine;

[Serializable]
public class GameStatistics
{
    [SerializeField] private float _lifeTimeSec;

    public float LifeTimeSec { get => _lifeTimeSec; set => _lifeTimeSec = value; }
}
