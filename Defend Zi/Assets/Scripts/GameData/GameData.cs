using Desdiene;

/* 
 * ��� (��)������������ ������������ NewtonsoftJson, 
 * ������� ��� ������ ������ ������ � ��������� � ����� public get � public set
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
