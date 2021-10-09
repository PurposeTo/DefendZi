using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class IronSourceAd : MonoBehaviourExt, IRewardedAd
{
    private const string AppKey = "113afc96d";

    protected override void AwakeExt()
    {
        IronSource.Agent.init(AppKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.validateIntegration();
        IronSource.Agent.shouldTrackNetworkState(true);
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private event Action<string> OnFailedToShow;
    private event Action OnRewarded;
    private event Action OnClosed;

    bool IRewardedAd.CanBeShown => IronSource.Agent.isRewardedVideoAvailable();

    event Action<string> IRewardedAd.OnFailedToShow
    {
        add => OnFailedToShow += value;
        remove => OnFailedToShow -= value;
    }

    event Action IRewardedAd.OnRewarded
    {
        add => OnRewarded += value;
        remove => OnRewarded -= value;
    }

    event Action IRewardedAd.OnClosed
    {
        add => OnClosed += value;
        remove => OnClosed -= value;
    }

    void IRewardedAd.Show()
    {
        IronSource.Agent.showRewardedVideo();
    }

    private void SubscribeEvents()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
    }

    private void UnsubscribeEvents()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent -= RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
    }

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
        Debug.Log($"Iron Source rewarded ad event: Opened");
    }

    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
        Debug.Log($"Iron Source rewarded ad event: Closed");
        OnClosed?.Invoke();
    }

    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
        Debug.Log($"Iron Source rewarded ad event:  Availability={rewardedVideoAvailability}");
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        Debug.Log($"Iron Source rewarded ad event: Rewarded.\n{placement}");
        OnRewarded?.Invoke();
    }

    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log($"Iron Source rewarded ad event:  ShowFailed.\n{error}");
        OnFailedToShow?.Invoke(error.ToString());
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
        Debug.Log($"Iron Source rewarded ad event: Started");
    }

    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
        Debug.Log($"Iron Source rewarded ad event: Ended");
    }

    //Invoked when the video ad is clicked. 
    void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        Debug.Log($"Iron Source rewarded ad event: Clicked\n{placement}");
    }

}
