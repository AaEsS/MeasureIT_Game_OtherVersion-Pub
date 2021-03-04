using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public static void SignIn()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success => { });
    }

    public static void AddScoreToLeaderboard(int score, string leaderboardId)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboard(string leaderboardId)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
