using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class SRTUnityAdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string GAME_ID = "3003911"; //replace with your gameID from dashboard. note: will be different for each platform.

    private string BANNER_PLACEMENT = "banner";
    private string INTERSTITAL_PLACEMENT = "video";
    private string REWARDED_VIDEO_PLACEMENT = "rewardedVideo";

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private bool testMode = true;

    private bool isInitialized = false;

    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    public void Initialize(string BANNER_PLACEMENT, string INTERSTITAL_PLACEMENT, string REWARDED_VIDEO_PLACEMENT, bool testMode)
    {
        if (!isInitialized) {
            if (Advertisement.isSupported)
            {
                DebugLog(Application.platform + " supported by Advertisement");
            }
            this.BANNER_PLACEMENT = BANNER_PLACEMENT;
            this.INTERSTITAL_PLACEMENT = INTERSTITAL_PLACEMENT;
            this.REWARDED_VIDEO_PLACEMENT = REWARDED_VIDEO_PLACEMENT;
            this.testMode = testMode;
            Advertisement.Initialize(GAME_ID, testMode, this);
            isInitialized = true;
        }
    }

    public void LoadBanner()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(BANNER_PLACEMENT, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(BANNER_PLACEMENT, options);
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(INTERSTITAL_PLACEMENT, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(INTERSTITAL_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
        if (placementId.Equals(REWARDED_VIDEO_PLACEMENT))
        {
            // give reward
            // trigger reward
        }
    }
    #endregion

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}

