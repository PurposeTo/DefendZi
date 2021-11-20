using System;

public class EmptyRewardedAdStub : IRewardedAd
{
    bool IRewardedAd.CanBeShown => true;

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

    event Action IRewardedAd.OnClosed
    {
        add { }
        remove { }
    }

    void IRewardedAd.Show() { }
}
