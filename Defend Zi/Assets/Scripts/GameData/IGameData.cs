using Desdiene.GameDataAsset.Data;

public interface IGameData : IData
{
    int MaxScore { get; }

    void SetMaxScore(uint score);
}
