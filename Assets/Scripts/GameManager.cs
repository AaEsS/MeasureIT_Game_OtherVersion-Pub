using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour, IUnityAdsListener
{
    public GameObject player, healthBar;
    public FacePlayer facePlayer;
    public InstantiateBullet instantiateBullet;
    public Animator gameplayeUIAnimator;
    public ButtonS buttonS;
    public Button continueByAd;

    private string gameId = "4034219";
    private string rewardedVideoId = "rewardedVideo";
    public bool testMode;

    public bool revived = false;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        continueByAd.interactable = Advertisement.IsReady(rewardedVideoId);
        Advertisement.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(rewardedVideoId))
            Advertisement.Show(rewardedVideoId);
        else Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == rewardedVideoId) continueByAd.interactable = true;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Advertisement.RemoveListener(this);
            Revive();
        }
        else if (showResult == ShowResult.Failed) Debug.LogWarning("The ad failed to be shown due to an error.");
    }

    public void Revive()
    {
        facePlayer.enabled = true;

        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Animator>().SetBool("Reviving", true);

        gameplayeUIAnimator.SetTrigger("TimeUp");
        GameObject.Find("AudioM").GetComponent<AudioSource>().UnPause();

        healthBar.GetComponent<Slider>().maxValue = 5;
        healthBar.GetComponent<Slider>().value = 5;
        player.GetComponent<Controls>().HP = (int)healthBar.GetComponent<Slider>().value;

        revived = true;
        buttonS.timeForContinue = 0f;
        Invoke("CanonFixed", 3f);
    }

    void CanonFixed() => instantiateBullet.enabled = true;

    public void OnUnityAdsDidError(string message)
    {
        // Show errors
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Indicate that the video started
    }
}
