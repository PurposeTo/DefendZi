using System;

public interface IRewardedAd
{
    event Action<string> OnFailedToShow;
    event Action OnRewarded;
    bool CanBeShown { get; }
    void Show();
}
