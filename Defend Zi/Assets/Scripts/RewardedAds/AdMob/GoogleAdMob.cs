using System;
using Desdiene.MonoBehaviourExtension;
using GoogleMobileAds.Api;
using UnityEngine;

/// <summary>
/// Need to be a global signleton
/// Need to be a monobehaviour
/// </summary>
public class GoogleAdMob : MonoBehaviourExt, IRewardedAd
{
    private readonly string _adMobId = new AdMobIdentificator().GetForTest();
    private RewardedAd _rewardedAd;

    protected override void AwakeExt()
    {
        MobileAds.Initialize(initStatus => { });
        _rewardedAd = CreateNew();
    }

    private Action OnFailedToShow;
    private Action OnRewarded;


    bool IRewardedAd.CanBeShown => CanBeShown;

    event Action IRewardedAd.OnFailedToShow
    {
        add => OnFailedToShow += value;
        remove => OnFailedToShow -= value;
    }

    event Action IRewardedAd.OnRewarded
    {
        add => OnRewarded += value;
        remove => OnRewarded -= value;
    }

    void IRewardedAd.Show()
    {
        if (CanBeShown)
        {
            _rewardedAd.Show();
        }
        else OnFailedToShow?.Invoke();
    }

    private bool CanBeShown => _rewardedAd.IsLoaded();

    private void UpdateAd()
    {
        DeleteOld(ref _rewardedAd);
        _rewardedAd = CreateNew();
    }

    private RewardedAd CreateNew()
    {
        RewardedAd rewardedAd = new RewardedAd(_adMobId);
        SubscribeEvents(rewardedAd);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);

        return rewardedAd;
    }

    private void DeleteOld(ref RewardedAd rewardedAd)
    {
        if (rewardedAd is null) throw new ArgumentNullException(nameof(rewardedAd));
        UnsubcribeEvents(rewardedAd);
        rewardedAd = null;
    }

    private void SubscribeEvents(RewardedAd rewardedAd)
    {
        if (rewardedAd is null) throw new ArgumentNullException(nameof(rewardedAd));

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void UnsubcribeEvents(RewardedAd rewardedAd)
    {
        if (rewardedAd is null) throw new ArgumentNullException(nameof(rewardedAd));

        rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log($"HandleRewardedAdFailedToLoad event received with message: {args.LoadAdError.GetMessage()}");
        // todo здесь нужно дождаться интернет соединения.
        UpdateAd();
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log($"HandleRewardedAdFailedToShow event received with message: {args.AdError.GetMessage()}");
        OnFailedToShow?.Invoke();
        UpdateAd();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
        UpdateAd();
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log($"HandleRewardedAdRewarded event received for {amount} {type}");
        OnRewarded?.Invoke();
    }
}
