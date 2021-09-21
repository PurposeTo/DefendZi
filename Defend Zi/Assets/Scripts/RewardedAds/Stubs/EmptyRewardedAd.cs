using System;

public class EmptyRewardedAd : IRewardedAd
{
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
