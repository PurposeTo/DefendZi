using Desdiene.GameDataAsset.Combiner;
using UnityEngine;

public class DataCombiner : IDataCombiner<GameData>
{
    GameData IDataCombiner<GameData>.Combine(GameData first, GameData second)
    {
        GameData gameData = new GameData();
        gameData.SetBestScore((uint)Mathf.Max(first.BestScore, second.BestScore));
        return gameData;
    }
}
