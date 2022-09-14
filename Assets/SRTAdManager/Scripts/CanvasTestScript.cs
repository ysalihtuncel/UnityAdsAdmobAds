using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTestScript : MonoBehaviour
{
    [SerializeField] AdSource BannerAD;
    [SerializeField] AdSource InterstitalAD;
    [SerializeField] AdSource RewardedAD;
    [SerializeField] int AdmobInterstitalCount;
    [SerializeField] int AdmobRewardCount;
    [SerializeField] int UnityInterstitalCount;
    [SerializeField] int UnityRewardCount;
    [SerializeField] string AdmobBannerID;
    [SerializeField] string AdmobInterstitalID;
    [SerializeField] string AdmobRewardID;
    [SerializeField] string UnityBannerID;
    [SerializeField] string UnityInterstitalID;
    [SerializeField] string UnityRewardID;

    [SerializeField] bool testMode = true;
    void Start() {
        SRTAdManager adManager = FindObjectOfType<SRTAdManager>();
        adManager.Initialize(
            BannerAD,
            InterstitalAD,
            RewardedAD,
            AdmobInterstitalCount,
            AdmobRewardCount,
            UnityInterstitalCount,
            UnityRewardCount,
            AdmobBannerID,
            AdmobInterstitalID,
            AdmobRewardID,
            UnityBannerID,
            UnityInterstitalID,
            UnityRewardID,
            testMode
        );
    }
    public void RewardOnClick() {
        SRTAdManager.ShowRewardedAd();
    }
    public void InterstitalOnClick() {
        SRTAdManager.ShowIntersititialAd();
    }
}
