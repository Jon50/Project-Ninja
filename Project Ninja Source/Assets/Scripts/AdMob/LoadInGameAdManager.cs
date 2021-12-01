using System;
using UnityEngine;
using GoogleMobileAds.Api;

using UnityEngine.UI;
using KadoNem.ProjectNinja.Managers;
using KadoNem.ProjectNinja.Locator;
using KadoNem.ProjectNinja.StateMachine;
using KadoNem.ProjectNinja.SceneLoadManagement;
using static KadoNem.ProjectNinja.Static.AdMobConstants;

public class LoadInGameAdManager : MonoBehaviour
{
    public GameObject rewardVideoButton;
    public SceneLoader sceneLoader;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd videoAd;
    private RewardedAd rewardedAd;

    private GameManager gameManager;

    public static LoadInGameAdManager Instance;

    private void Awake()
    {
        Instance = this;

        RequestBanner();
        RequestInterstitial();
        RequestRewarded();
        RequestVideo();
    }

    private void Start()
    {
        gameManager = ServiceLocator.Resolve<GameManager>();
    }

    private void OnEnable()
    {
        interstitialAd.OnAdClosed += HandleInterstitialAdClosed;

        videoAd.OnAdOpening += HandleInterstitialAdOpening;
        videoAd.OnAdClosed += HandleInterstitialAdClosed;

        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }


    private void OnDisable()
    {
        interstitialAd.OnAdClosed -= HandleInterstitialAdClosed;

        videoAd.OnAdOpening -= HandleInterstitialAdOpening;
        videoAd.OnAdClosed -= HandleInterstitialAdClosed;

        rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }

    private void RequestBanner()
    {
        bannerView = new BannerView(BANNER_AD_ID, AdSize.SmartBanner, AdPosition.Top);

        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
    }

    private void RequestInterstitial()
    {
        interstitialAd = new InterstitialAd(INTERSTITIAL_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    private void RequestRewarded()
    {
        rewardedAd = new RewardedAd(REWARDED_VIDEO_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
    }

    private void RequestVideo()
    {
        videoAd = new RewardedAd(REWARDED_VIDEO_AD_ID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        videoAd.LoadAd(adRequest);
    }

    public void LoadInterstitialAd()
    {
        if(interstitialAd.IsLoaded())
        {
            PauseGame();
            interstitialAd.Show();
        }
    }

    public void LoadVideoAd()
    {
        if(rewardedAd.IsLoaded())
            videoAd.Show();
    }

    public void CheckForRewardedAd()
    {
        if(rewardedAd.IsLoaded())
        {
            rewardVideoButton.SetActive(true);
            rewardVideoButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                LoadRewardedAd();
            });
        }
        else
        {
            rewardVideoButton.SetActive(false);
        }

        void LoadRewardedAd()
        {
            rewardedAd.Show();
        }
    }

    private void HandleInterstitialAdOpening( object sender, EventArgs e ) => PauseGame();

    private void HandleInterstitialAdClosed( object sender, EventArgs e ) => UnpauseGame(); //TODO: Apperently this is running on seperate threads, that's a problem!

    private void HandleRewardedAdOpening( object sender, EventArgs e ) => PauseGame();

    private void HandleRewardedAdClosed( object sender, EventArgs e )
    {
        UnpauseGame();
        Reward();
    }


    private void PauseGame()
    {
        gameManager.SetNewState(GamePauseState.Instance); //TODO: Maybe change logic here and use update to run on main thread.
    }

    private void UnpauseGame()
    {
        gameManager.SetNewState(GamePlayingState.Instance);
    }

    private void Reward()
    {
        rewardVideoButton.SetActive(false);
    }
}
