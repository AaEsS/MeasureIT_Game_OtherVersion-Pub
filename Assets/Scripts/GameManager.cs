using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;

[RequireComponent(typeof(Button))]
public class GameManager : MonoBehaviour, IUnityAdsListener
{
    public GameObject player, healthBar;
    public FacePlayer facePlayer;
    public InstantiateBullet instantiateBullet;
    public Animator gameplayUIAnimator;
    public ButtonS buttonS;
    public TextMeshProUGUI continueByAdText;
    public Powerups powerupsScript;

#if UNITY_ANDROID
    private string gameId = "4034219";
#endif

    Button continueByAd;
    private string rewardedVideoId = "rewardedVideo";
    public bool testMode;

    public bool revived = false;

    void Awake()
    {
        continueByAd = GetComponent<Button>();
        continueByAd.interactable = Advertisement.IsReady(rewardedVideoId);

        if (continueByAd) continueByAd.onClick.AddListener(ShowRewardedVideo);
        
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (continueByAd.interactable) continueByAdText.SetText($"<size=%70>Continue?</size>\n<size=%40>(Ad)</size>");
        else continueByAdText.SetText($"<size=%70>Continue?</size>\n<size=%40>(Ad)</size> <size=%30>network error</size>");
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedVideoId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == rewardedVideoId) continueByAd.interactable = true;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Revive();
        }
        else if (showResult == ShowResult.Skipped)
        {
            buttonS.ShowScore();
        }
        else if (showResult == ShowResult.Failed)
        {
            buttonS.ShowScore();
            Debug.LogWarning("The ad failed to be shown due to an error.");
        }
    }

    public void Revive()
    {
        facePlayer.enabled = true;

        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Animator>().SetBool("Reviving", true);
        foreach (GameObject powerupB in GameObject.FindGameObjectsWithTag("PowerupButton"))
            powerupB.GetComponent<Button>().interactable = true;

        gameplayUIAnimator.SetTrigger("TimeUp");

        GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiLoop.UnPause();

        healthBar.GetComponent<Slider>().maxValue = 5;
        healthBar.GetComponent<Slider>().value = 5;
        player.GetComponent<Controls>().HP = (int)healthBar.GetComponent<Slider>().value;

        powerupsScript.enabled = true;

        revived = true;
        Invoke("CanonFixed", 3f);
    }

    void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    void CanonFixed() => instantiateBullet.enabled = true;

    public void OnUnityAdsDidError(string message)
    {
        // Errors
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Indicate that the video started
    }
}
