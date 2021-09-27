using System;

public class EmptyRewardedAd : IRewardedAd
{
    bool IRewardedAd.CanBeShown => false;
    
    event Action IRewardedAd.OnFailedToShow
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
