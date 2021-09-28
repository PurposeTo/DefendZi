using System;

public class SuccessRewardedAdStub : IRewardedAd
{
    private event Action OnRewarded;

    bool IRewardedAd.CanBeShown => true;
    
    event Action<string> IRewardedAd.OnFailedToShow
    {
        add { }
        remove { }
    }

    event Action IRewardedAd.OnRewarded
    {
        add => OnRewarded += value;
        remove => OnRewarded -= value;
    }

    event Action IRewardedAd.OnClosed
    {
        add => OnRewarded += value;
        remove => OnRewarded -= value;
    }

    void IRewardedAd.Show()
    {
        OnRewarded?.Invoke();
    }
}
