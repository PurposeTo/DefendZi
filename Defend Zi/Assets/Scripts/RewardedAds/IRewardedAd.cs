using System;

public interface IRewardedAd
{
    event Action<string> OnFailedToShow;
    event Action OnRewarded;
    event Action OnClosed;
    bool CanBeShown { get; }
    void Show();
}
