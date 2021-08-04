using Desdiene;

/* 
 * Для (де)сериализации используется NewtonsoftJson, 
 * поэтому все данные должны лежать в свойствах и иметь public get и public set
 */
public class GameData : IGameData
{
    public int BestScore { get; set; } = 0;

    public void SetBestScore(uint score)
    {
        BestScore = Math.ClampMin((int)score, BestScore);
    }

    public override string ToString()
    {
        return $"{GetType().Name}\n"
             + $"BestScore={BestScore}";
    }
}
