using Desdiene.GameDataAsset.Data;

public interface IGameData : IData
{
    int BestScore { get; }

    void SetBestScore(uint score);
}
