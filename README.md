# Unity Ads and Admob Ads same time

### Required
#### Unity Advertisement 4.1.3  
#### GoogleMobileAds-v7.0.0.unitypackage  


A script that allows Unity Ads and Admob to be used together. It is not waterfall or meditation. In order to use this, you must have the Advertisment and GoogleMobileAds plugins in your Unity project.  
  
  
### Usage:
#### Initialize SRTAdManager with:
      
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
      
      
## How to show ads? 

#### Interstital ad

SRTAdManager.ShowIntersititialAd();

#### Rewarded Ad

SRTAdManager.ShowRewardedAd();
      
