using System;

public class SuccessRewardedAd : IRewardedAd
{
    private event Action OnRewarded;

    bool IRewardedAd.CanBeShown => true;
    
    event Action IRewardedAd.OnFailedToShow
    {
        add { }
        remove { }
    }

    event Action IRewardedAd.OnRewarded
    {
        add => OnRewarded += value;
        remove => OnRewarded -= value;
    }

    void IRewardedAd.Show()
    {
        OnRewarded?.Invoke();
    }
}
