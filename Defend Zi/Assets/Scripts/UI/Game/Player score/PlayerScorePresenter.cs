using UnityEngine;

[RequireComponent(typeof(PlayerScoreView))]
public class PlayerScorePresenter : MonoBehaviour
{
    private PlayerScoreView scoreView;
    private IStat<int> playerScore;

    private void Awake()
    {
        scoreView = GetComponent<PlayerScoreView>();
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            playerScore = GameObjectsHolder.Instance.PlayerPresenter.Score;
            UpdateValueView();
            playerScore.OnStatChange += UpdateValueView;
        };
    }


    private void OnDestroy()
    {
        playerScore.OnStatChange -= UpdateValueView;
    }


    private void UpdateValueView()
    {
        scoreView.SetScore(playerScore.Value);
    }
}
