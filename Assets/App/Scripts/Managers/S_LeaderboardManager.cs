using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class S_LeaderboardManager : MonoBehaviour
{
    private const string LeaderboardId = "Scores";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();

        SetName("Anonymous");
    }

    private async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () => {};
        AuthenticationService.Instance.SignInFailed += s => {};

       await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void SetName(string name)
    { 
        var nameResponse = await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
        Debug.Log(JsonConvert.SerializeObject(nameResponse));
    }

    public async void AddScore(int value)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, value);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));

        Reset();
    }

    public async void Reset()
    {
        AuthenticationService.Instance.SignOut();
        AuthenticationService.Instance.ClearSessionToken();

        await SignInAnonymously();

        SetName("Anonymous");
    }

    private async void GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async void GetPaginatedScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions{Offset = 0, Limit = 10});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    private async void GetPlayerScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    private async void GetPlayerRange()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(LeaderboardId, new GetPlayerRangeOptions{RangeLimit = 10});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    private async void GetScoresByPlayerIds()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, new());
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }
}
