using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRTAdManager : MonoBehaviour
{
    public static SRTAdManager instance;

    private SRTUnityAdManager unityAdManager;
    private SRTAdmobAdManager admobAdManager;

    [SerializeField] AdSource interstitalSource;
    [SerializeField] AdSource rewardedSource;

    int AdmobInterstitalCounter = 0, UnityInterstitalCounter = 0;
    int AdmobRewardedCounter = 0, UnityRewardedCounter = 0;
    [SerializeField] int AdmobInterstitalCount = 0, UnityInterstitalCount = 0;
    [SerializeField] int AdmobRewardedCount = 0, UnityRewardedCount = 0;
    private bool isInitialized = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            unityAdManager = GetComponent<SRTUnityAdManager>();
            admobAdManager = GetComponent<SRTAdmobAdManager>();
            if (unityAdManager == null)
            {
                unityAdManager = FindObjectOfType<SRTUnityAdManager>();
            }
            if (admobAdManager == null)
            {
                admobAdManager = FindObjectOfType<SRTAdmobAdManager>();
            }
            //ADMOB
        }
    }
    /// <summary>
    /// U = UNITY, A = ADMOB
    /// </summary>
    /// <param name="banner">Banner Ad Type</param>
    /// <param name="interstitial">Interstital Ad Type</param>
    /// <param name="rewarded">Rewarded Ad Type</param>
    /// <param name="AInterstitalCount">if ad type mixed: How many time/times show Admob interstital ad</param>
    /// <param name="ARewardedCount">if ad type mixed: How many time/times show Admob rewarded ad</param>
    /// <param name="UInterstitalCount">if ad type mixed: How many time/times show Unity interstital ad</param>
    /// <param name="URewardedCount">if ad type mixed: How many time/times show Unity rewarded ad</param>
    /// <param name="ABannerID">Admob Banner ID</param>
    /// <param name="AInterstitialID">Admob Interstital ID</param>
    /// <param name="ARewardedID">Admob Rewarded ID</param>
    /// <param name="UBannerID">Unity Banner ID</param>
    /// <param name="UInterstitialID">Unity Interstital ID</param>
    /// <param name="URewardedID">Unity Rewarded ID</param>
    /// <param name="testMode">Ads Mode if is test set true</param>
    public static void Initialize(AdSource banner, AdSource interstitial, AdSource rewarded,
                    int AInterstitalCount, int ARewardedCount,
                    int UInterstitalCount, int URewardedCount,
                    string ABannerID, string AInterstitialID, string ARewardedID,
                    string UBannerID, string UInterstitialID, string URewardedID, bool testMode)
    {
        if (instance != null)
        {
            if (!instance.isInitialized) {
                instance.isInitialized = true;
                instance.interstitalSource = interstitial;
                instance.rewardedSource = rewarded;
                if (banner == AdSource.Mixed) {
                    banner = AdSource.UnityAds;
                }
                //INITIALIZE FIELD
                switch (interstitial)
                {
                    case AdSource.Mixed:
                        instance.AdmobInterstitalCount = AInterstitalCount;
                        instance.UnityInterstitalCount = UInterstitalCount;
                        instance.unityAdManager.Initialize(instance.SRTDecoder(UBannerID), instance.SRTDecoder(UInterstitialID), instance.SRTDecoder(URewardedID), testMode);
                        instance.admobAdManager.Initialize(instance.SRTDecoder(ABannerID), instance.SRTDecoder(AInterstitialID), instance.SRTDecoder(ARewardedID), testMode);
                        // ADMOB initialize
                        break;
                    case AdSource.AdmobAds:
                        instance.admobAdManager.Initialize(instance.SRTDecoder(ABannerID), instance.SRTDecoder(AInterstitialID), instance.SRTDecoder(ARewardedID), testMode);
                        // ADMOB initialize
                        break;
                    case AdSource.UnityAds:
                        instance.unityAdManager.Initialize(instance.SRTDecoder(UBannerID), instance.SRTDecoder(UInterstitialID), instance.SRTDecoder(URewardedID), testMode);
                        break;
                    default:
                        break;
                }
                //END INITIALIZE
                if (instance.rewardedSource == AdSource.Mixed) {
                    instance.AdmobRewardedCount = ARewardedCount;
                    instance.UnityRewardedCount = URewardedCount;
                }
    
                //BANNER LOAD FIELD
                switch (banner)
                {
                    case AdSource.AdmobAds:
                        instance.LoadAdmobBannerAD();
                        break;
                    case AdSource.UnityAds:
                        instance.LoadUnityBannerAD();
                        break;
                    default:
                        break;
                }
                //END BANNER LOAD
    
                //INTERSTITAL LOAD FIELD
                switch (interstitial)
                {
                    case AdSource.Mixed:
                        instance.LoadAdmobInterstitalAD();
                        instance.LoadUnityInterstitalAD();
                        break;
                    case AdSource.AdmobAds:
                        instance.LoadAdmobInterstitalAD();
                        break;
                    case AdSource.UnityAds:
                        instance.LoadUnityInterstitalAD();
                        break;
                    default:
                        break;
                }
                //END INTERSTITIAL FIELD
    
                //REWARDED LOAD FIELD
                switch (rewarded)
                {
                    case AdSource.Mixed:
                        instance.LoadAdmobRewardedAD();
                        instance.LoadUnityRewardedAD();
                        break;
                    case AdSource.AdmobAds:
                        instance.LoadAdmobRewardedAD();
                        break;
                    case AdSource.UnityAds:
                        instance.LoadUnityRewardedAD();
                        break;
                    default:
                        break;
                }
                //REWARDED INTERSTITIAL FIELD
            }
        }
    }


    //AD LOAD FIELD

    //UNITY ADS FIELD
    private void LoadUnityBannerAD()
    {
        unityAdManager.LoadBanner();
    }

    private void LoadUnityInterstitalAD()
    {
        unityAdManager.LoadNonRewardedAd();
    }
    private void LoadUnityRewardedAD()
    {
        unityAdManager.LoadRewardedAd();
    }
    //UNITY ADS END

    private void LoadAdmobBannerAD()
    {
        admobAdManager.LoadAdmobBanner();
    }
    private void LoadAdmobInterstitalAD()
    {
        admobAdManager.LoadAdmobInterstitial();
    }
    private void LoadAdmobRewardedAD()
    {
        admobAdManager.LoadAdmobRewarded();
    }
    //AD LOAD END

    //AD SHOW FIELD

    //INTERSTITAL AD SHOW FIELD
    public static void ShowIntersititialAd()
    {
        switch (instance.interstitalSource) {
            case AdSource.AdmobAds:
                instance.admobAdManager.ShowInterstitialAdmob();
                break;
            case AdSource.UnityAds:
                instance.unityAdManager.ShowNonRewardedAd();
                break;
            case AdSource.Mixed:
                if (instance.AdmobInterstitalCounter < instance.AdmobInterstitalCount) {
                    instance.admobAdManager.ShowInterstitialAdmob();
                    instance.AdmobInterstitalCounter++;
                }
                else if (instance.UnityInterstitalCounter < instance.UnityInterstitalCount) {
                    instance.unityAdManager.ShowNonRewardedAd();
                    instance.UnityInterstitalCounter++;
                }
                else {
                    //Reset counter
                    instance.AdmobInterstitalCounter = 1;
                    instance.UnityInterstitalCounter = 0;
                    instance.admobAdManager.ShowInterstitialAdmob();
                }
                break;
            default:
                break;
        }
    }
    //INTERSTITAL AD SHOW END

    //REWARDED AD SHOW FIELD
    public static void ShowRewardedAd()
    {
        switch (instance.rewardedSource) {
            case AdSource.AdmobAds:
                instance.admobAdManager.ShowAdmobRewarded();
                break;
            case AdSource.UnityAds:
                instance.unityAdManager.ShowRewardedAd();
                break;
            case AdSource.Mixed:
                if (instance.AdmobRewardedCounter < instance.AdmobInterstitalCount) {
                    instance.admobAdManager.ShowAdmobRewarded();
                    instance.AdmobRewardedCounter++;
                }
                else if (instance.UnityRewardedCounter < instance.UnityRewardedCount) {
                    instance.unityAdManager.ShowRewardedAd();
                    instance.UnityRewardedCounter++;
                }
                else {
                    //Reset counter
                    instance.AdmobRewardedCounter = 1;
                    instance.UnityRewardedCounter = 0;
                    instance.admobAdManager.ShowAdmobRewarded();
                }
                break;
            default:
                break;
        }
    }
    //REWARDED AD SHOW END
    //AD SHOW END

    private string SRTDecoder(string EncodedString)
    {
        //TODO:
        return EncodedString;
    }
}

public enum AdSource { UnityAds, AdmobAds, Mixed, None, }
