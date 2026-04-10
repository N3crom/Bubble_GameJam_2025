using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class S_LeaderboardManager : MonoBehaviour
{
    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnSetName rseOnSetName;

    [TabGroup("Outputs")]
    [SerializeField] private RSE_OnUpdateLeaderboard rseOnUpdateLeaderboard;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_Leaderboard rsoLeaderboard;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_PlayerName rsoPlayer;

    private const string LeaderboardId = "scores";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();

        SetName("Anonymous");

        GetPaginatedScores();
    }

    private void OnEnable()
    {
        rseOnSetName.action += SetName;
    }

    private void OnDisable()
    {
        rseOnSetName.action -= SetName;

        rsoLeaderboard.Value.Clear();
    }

    private async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () => { };
        AuthenticationService.Instance.SignInFailed += s => { };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void SetName(string name)
    {
        var nameResponse = await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
        rsoPlayer.Value = name;
    }

    private async void AddScore(int value)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, value);

        Reset();
    }

    private async void Reset()
    {
        AuthenticationService.Instance.SignOut();
        AuthenticationService.Instance.ClearSessionToken();

        await SignInAnonymously();

        SetName("Anonymous");
    }

    private async void GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
    }

    private async void GetPaginatedScores()
    {
        rsoLeaderboard.Value.Clear();

        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = 0, Limit = 10 });
        List<S_ClassPlayer> result = new();

        foreach (var entry in scoresResponse.Results)
        {
            S_ClassPlayer data = new S_ClassPlayer
            {
                rank = entry.Rank,
                name = entry.PlayerName,
                score = (int)entry.Score,
            };

            result.Add(data);
        }

        rsoLeaderboard.Value = result;

        rseOnUpdateLeaderboard.Call();
    }

    private async void GetPlayerScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
    }

    private async void GetPlayerRange()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(LeaderboardId, new GetPlayerRangeOptions { RangeLimit = 10 });
    }

    private async void GetScoresByPlayerIds()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, new());
    }
}