using System;

public class FailRewardedAd : IRewardedAd
{
    private event Action<string> OnFailedToShow;

    event Action<string> IRewardedAd.OnFailedToShow
    {
        add => OnFailedToShow += value;
        remove => OnFailedToShow -= value;
    }

    event Action IRewardedAd.OnRewarded
    {
        add { }
        remove { }
    }

    bool IRewardedAd.CanBeShown => true;

    void IRewardedAd.Show()
    {
        OnFailedToShow?.Invoke("Stub error");
    }
}
