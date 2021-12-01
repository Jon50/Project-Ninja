using System;
using UnityEngine;
using GoogleMobileAds.Api;
using static KadoNem.ProjectNinja.Static.AdMobConstants;

using Random = UnityEngine.Random;

public class GoogleAds : MonoBehaviour
{
    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _popupVideoAd;
    private RewardedAd _shopVideoAd;
    private static bool _hasInitialized = false;
    private float _rewardInternalTimer;
    private bool _newPopupRequest = true;
    private bool _newShopVideoRequest = true;
    private bool _newInterstitialRequest = true;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        if(!_hasInitialized)
        {
            MobileAds.Initialize(initStatus => { });
            _hasInitialized = true;
        }

        _rewardInternalTimer = Random.Range(5f, 10f);

        RequestBanner();
        RequestInterstitial();
        RequestShopVideoReward();
        RequestPopupVideoReward();
    }


    private void OnEnable()
    {
    }


    private void OnDisable()
    {
    }


    private void Update()
    {
        //Request timers
        if(_newPopupRequest)
        {
            _rewardInternalTimer -= Time.deltaTime;

            if(_rewardInternalTimer <= 0f)
            {
                if(_popupVideoAd.IsLoaded())
                {
                    _newPopupRequest = false;
                }
            }
        }

        if(_newShopVideoRequest)
        {
            if(_shopVideoAd.IsLoaded())
                _newShopVideoRequest = false;
            //Needs a timer which will enable the ad button again
        }

        if(_newInterstitialRequest)
        {
            if(_interstitialAd.IsLoaded())
                _newInterstitialRequest = false;
            //Needs a timer which will enable the ad button again
        }
    }

    public void ShowRewardedVideoAd( int rewardValue = 0 )
    {
        _popupVideoAd.Show();
    }

    public void ShowInterstitialAd( int rewardValue = 0 )
    {
        _interstitialAd.Show();
    }

    public void ShowShopVideoAd( int rewardValue = 0 )
    {
        _shopVideoAd.Show();
    }

    private void RequestBanner()
    {
        //AdSize adSize = new AdSize(725, 90);
        _bannerView = new BannerView(BANNER_AD_ID, AdSize.SmartBanner, AdPosition.Top);

        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
    }

    private void RequestInterstitial()
    {
        _interstitialAd = new InterstitialAd(INTERSTITIAL_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);

        _interstitialAd.OnAdClosed += EnergyRewardAndStartNew;

        _newInterstitialRequest = true;
    }

    private void RequestPopupVideoReward()
    {
        _popupVideoAd = new RewardedAd(REWARDED_VIDEO_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        _popupVideoAd.LoadAd(adRequest);

        _popupVideoAd.OnUserEarnedReward += CoinPopupRewardAndStartNew;
        _popupVideoAd.OnAdFailedToShow += HandleFailedToShow;

        _newPopupRequest = true;
    }

    private void RequestShopVideoReward()
    {
        _shopVideoAd = new RewardedAd(REWARDED_VIDEO_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        _shopVideoAd.LoadAd(adRequest);

        _shopVideoAd.OnUserEarnedReward += CoinShopRewardAndStartNew;

        _newShopVideoRequest = true;
    }

    private void HandleFailedToShow( object sender, EventArgs e )
    {
        RequestPopupVideoReward();
    }

    private void CoinPopupRewardAndStartNew( object sender, EventArgs e )
    {
        RequestPopupVideoReward();
    }

    private void CoinShopRewardAndStartNew( object sender, EventArgs e )
    {
        RequestShopVideoReward();
    }

    private void EnergyRewardAndStartNew( object sender, EventArgs e )
    {
        RequestInterstitial();
    }

    private void ToggleEnergyButton( bool state )
    {
    }
}