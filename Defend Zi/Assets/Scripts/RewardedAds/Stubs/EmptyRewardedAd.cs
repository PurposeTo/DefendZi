using System;

public class EmptyRewardedAd : IRewardedAd
{
    bool IRewardedAd.CanBeShown => false;
    
    event Action<string> IRewardedAd.OnFailedToShow
    {
        add { }
        remove { }
    }

    event Action IRewardedAd.OnRewarded
    {
        add { }
        remove { }
    }

    void IRewardedAd.Show() { }
}
