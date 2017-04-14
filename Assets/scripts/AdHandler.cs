using UnityEngine;
using UnityEngine.Advertisements;

public class AdHandler : MonoBehaviour
{
    public static AdHandler adHandler;

    public string iosId_UnityAds, androidId_UnityAds;

    void Awake()
    {
        adHandler = this;

#if UNITY_ANDROID
        Advertisement.Initialize(androidId_UnityAds);
#elif UNITY_IPHONE
        Advertisement.Initialize(iosId_UnityAds);
#endif
    }

    public bool ShowRewardedVideoAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);

            return true;
        }
        else
        {
            return false;
        }
    }


    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
