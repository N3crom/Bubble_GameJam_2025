using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_LeaderboardDisplay : MonoBehaviour
{
    [TabGroup("References")]
    [Title("Texts")]
    [SerializeField] private List<TextMeshProUGUI> textRank;

    [TabGroup("References")]
    [SerializeField] private List<TextMeshProUGUI> textName;

    [TabGroup("References")]
    [SerializeField] private List<TextMeshProUGUI> textScore;

    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnUpdateLeaderboard rseOnUpdateLeaderboard;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_Leaderboard rsoLeaderboard;

    private void OnEnable()
    {
        rseOnUpdateLeaderboard.action += LoadEntries;
    }

    private void OnDisable()
    {
        rseOnUpdateLeaderboard.action -= LoadEntries;
    }

    public void LoadEntries()
    {
        for (int i = 0; i < 10; i++)
        {
            textRank[i].text = (i + 1).ToString();

            if (rsoLeaderboard.Value != null && i < rsoLeaderboard.Value.Count)
            {
                var entry = rsoLeaderboard.Value[i];

                textName[i].text = entry.name.Split('#')[0];
                textScore[i].text = entry.score.ToString();
            }
            else
            {
                textName[i].text = "Empty";
                textScore[i].text = "0";
            }
        }
    }
}