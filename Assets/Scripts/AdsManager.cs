using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {

    // "ca-app-pub-3940256099942544/6300978111" // Test Banner Ad Id
    // "ca-app-pub-2522887740521329/2425919720" // REAL Banner Ad Id(Use only at the time of releasing the game)
    string bannerAdId = "ca-app-pub-2522887740521329/2425919720";

    // "ca-app-pub-3940256099942544/1033173712" // Test Interstital Ad Id
    // "ca-app-pub-2522887740521329/9740129500" // REAL Interstital Ad Id(Use only at the time of releasing the game)
    string interstitialAdId = "ca-app-pub-2522887740521329/9740129500";
    public BannerView bannerAd;
    public InterstitialAd interstitialAd;
    public bool isAdShown = false;

    void Start() {
        // MobileAds Initialization
        MobileAds.Initialize((initStatus) => {});
        if(SceneManager.GetActiveScene().name == "Main") {
            requestBannerAd();
            requestInterstitialAd();
        }
    }

    // BANNER ADs
    void requestBannerAd() {
        bannerAd = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        bannerAd.LoadAd(request);

        // Called when an Banner Ad request has successfully loaded
        bannerAd.OnAdLoaded += HandleOnBannerAdLoaded;
        // Called when an Banner Ad request has failed to load
        bannerAd.OnAdFailedToLoad += HandleOnBannerAdFailedToLoad;
    }

    void hideBannerAd() {
        bannerAd.Hide();
        Debug.Log("Banner Ad Hidden");
    }
    public void HandleOnBannerAdLoaded(object sender, EventArgs args) {
        Debug.Log("Banner Ad Loaded Successfully!");
    } 

    public void HandleOnBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        Debug.Log("Banner Ad Failed to Load!" + args.LoadAdError);
    }

    // INTERSTITIAL ADs
    public void requestInterstitialAd() {
        interstitialAd = new InterstitialAd(interstitialAdId);
        
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);

        // Called when an Interstitial Ad request has successfully loaded
        // this.interstitialAd.OnAdLoaded += HandleOnInterstitialAdLoaded;
        // // Called when an Interstitial Ad request has failed to load
        this.interstitialAd.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;
        // // Called when an Interstitial Ad is closed
        this.interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
    }

    public void showInterstitialAd() {
        if(!interstitialAd.IsLoaded()) {
            SceneManager.LoadScene("Main");
            interstitialAd.Destroy();
        } else {
            interstitialAd.Show();
        }
    }

    // public void HandleOnInterstitialAdLoaded(object sender, EventArgs args) {
    //     interstitialAd.Show();
    //     Debug.Log("Interstitial Ad Loaded Successfully!");
    // }

    public void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        Debug.Log("Interstitial Ad Failed to Load!" + args.LoadAdError);
    }
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args) {
        SceneManager.LoadScene("Main");
        interstitialAd.Destroy();
        Debug.Log("Interstitial Ad Closed!");
    }
}
