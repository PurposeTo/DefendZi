using System;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(ScoreAdderByTime))]
public class Player : MonoBehaviourExt, IUserControlled
{
    private IUserControlled control;

    protected override void AwakeExt()
    {
        IPosition position = GetComponent<PlayerPosition>();
        control = GetComponent<PlayerControl>().Constructor(position);
        IScoreCollector collector = GetComponent<PlayerScore>();
        GetComponent<ScoreAdderByTime>().Constructor(collector);
    }



    void IUserControlled.Control(IUserInput input) => control.Control(input);
}
