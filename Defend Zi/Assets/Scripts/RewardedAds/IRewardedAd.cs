using System;

public interface IRewardedAd
{
    event Action OnFailedToShow;
    event Action OnRewarded;
    void Show();
}
