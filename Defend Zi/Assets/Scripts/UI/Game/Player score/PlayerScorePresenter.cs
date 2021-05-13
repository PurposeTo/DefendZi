using UnityEngine;

[RequireComponent(typeof(PlayerScoreView))]
public class PlayerScorePresenter : MonoBehaviour
{
    private PlayerScoreView scoreView;

    private void Awake()
    {
        scoreView = GetComponent<PlayerScoreView>();
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            IStat<int> playerScore = GameObjectsHolder.Instance.PlayerPresenter.Score;
            scoreView.Constructor(playerScore);
        };
    }
}
