using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class SRTAdmobAdManager : MonoBehaviour
{

    private string BANNER_PLACEMENT = "ca-app-pub-3940256099942544/6300978111";
    private string INTERSTITAL_PLACEMENT = "ca-app-pub-3940256099942544/1033173712";
    private string REWARDED_VIDEO_PLACEMENT = "ca-app-pub-3940256099942544/5224354917";

    private bool isInitialized = false;
    private bool testMode = true;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    public AdPosition bannerPosition = AdPosition.Bottom;

    // Start is called before the first frame update
    public void Initialize(string BANNER_PLACEMENT, string INTERSTITAL_PLACEMENT, string REWARDED_VIDEO_PLACEMENT, 
                    bool testMode)
    {
        if (!isInitialized)
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(initStatus => { });
            isInitialized = true;
            this.testMode = testMode;
            if (!testMode)
            {
                this.BANNER_PLACEMENT = BANNER_PLACEMENT;
                this.INTERSTITAL_PLACEMENT = INTERSTITAL_PLACEMENT;
                this.REWARDED_VIDEO_PLACEMENT = REWARDED_VIDEO_PLACEMENT;
            }
        }
    }

    private AdRequest AdmobAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    // Banner ADMOB
    public void LoadAdmobBanner()
    {
        this.bannerView = new BannerView(BANNER_PLACEMENT, AdSize.Banner, bannerPosition);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;

        this.bannerView.LoadAd(AdmobAdRequest());
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void DestroyAdmobBanner()
    {
        if (bannerView == null)
        {
            return;
        }
        bannerView.Hide();
        bannerView.Destroy();
    }

    // Interstital ADMOB
    public void LoadAdmobInterstitial()
    {
        this.interstitial = new InterstitialAd(INTERSTITAL_PLACEMENT);
        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnInterstitalAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnInterstitalAdFailedToLoad;
        this.interstitial.OnAdFailedToShow += HandleOnInterstitalAdFailedToShow;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnInterstitalAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnInterstitalAdClosed;
        this.interstitial.LoadAd(AdmobAdRequest());
    }

    public void HandleOnInterstitalAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnInterstitalAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
    }

    public void HandleOnInterstitalAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetMessage());
    }

    public void HandleOnInterstitalAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");

    }

    public void HandleOnInterstitalAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
        LoadAdmobInterstitial();

    }

    public bool IsLoadedInterstitalAdmob()
    {
        if (this.interstitial == null)
        {
            return false;
        }
        return this.interstitial.IsLoaded();
    }

    public void ShowInterstitialAdmob()
    {
        if (IsLoadedInterstitalAdmob())
        {
            this.interstitial.Show();
        }
    }


    // Rewarded ADMOB

    public void LoadAdmobRewarded()
    {
        this.rewardedAd = new RewardedAd(REWARDED_VIDEO_PLACEMENT);
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(AdmobAdRequest());

    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                            );
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetMessage());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        this.LoadAdmobRewarded();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //TODO: On reward
    }

    public bool IsLoadedAdmobRewarded()
    {
        if (this.rewardedAd == null)
        {
            return false;
        }
        return this.rewardedAd.IsLoaded();
    }

    public void ShowAdmobRewarded()
    {
        if (IsLoadedAdmobRewarded())
        {
            this.rewardedAd.Show();
        }
    }

}
