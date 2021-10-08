using System;
using UnityEngine;

[Serializable]
public class GameStatistics
{
    [SerializeField] private TimeSpan _lifeTime;

    public TimeSpan LifeTime { get => _lifeTime; set => _lifeTime = value; }
}
