using System;

public class FailRewardedAd : IRewardedAd
{
    private event Action OnFailedToShow;

    event Action IRewardedAd.OnFailedToShow
    {
        add => OnFailedToShow += value;
        remove => OnFailedToShow -= value;
    }

    event Action IRewardedAd.OnRewarded
    {
        add { }
        remove { }
    }

    void IRewardedAd.Show()
    {
        OnFailedToShow?.Invoke();
    }
}
