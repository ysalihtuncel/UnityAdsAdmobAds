# UnityAdsAdmobAds
Unity Ads and Admob Ads same time 


# Usage:
Initialize SRTAdManager with:
      
	SRTAdManager.Initialize(  
		BannerAD, //Banner Ad type Unity or Admob  
		InterstitalAD, //Interstital Ad type Unity, Admob or Mixed  
		RewardedAD, //Rewarded Ad type Unity, Admob or Mixed  
		AdmobInterstitalCount, // if interstital ad type mixed: How many time/times show Admob interstital ad  
		AdmobRewardCount, // if rewarded ad type mixed: How many time/times show Admob rewarded ad  
		UnityInterstitalCount, // if interstital ad type mixed: How many time/times show Unity interstital ad  
		UnityRewardCount, // if rewarded ad type mixed: How many time/times show Unity rewarded ad  
		AdmobBannerID, // Admob Banner Ad ID  
		AdmobInterstitalID, // Admob Interstital Ad ID  
		AdmobRewardID, // Admob Rewarded Ad ID  
		UnityBannerID, // Unity Banner Ad ID  
		UnityInterstitalID, // Unity Interstital Ad ID  
		UnityRewardID, // Unity Rewarded Ad ID  
		testMode // Ads Mode if you are testing set true  
	);  
      
      
      Showing interstital AD:  
      SRTAdManager.ShowIntersititialAd();  
        
      Showing rewarded AD:  
      SRTAdManager.ShowRewardedAd();  
      
