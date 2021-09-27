using System;

public interface IRewardedAd
{
    event Action OnFailedToShow;
    event Action OnRewarded;
    bool CanBeShown { get; }
    void Show();
}
