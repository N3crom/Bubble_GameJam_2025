using System.Collections.Generic;
using UnityEngine;

using Dan.Main;
using TMPro;

public class S_LeaderboardShow : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> textRank;
    [SerializeField] private List<TextMeshProUGUI> textName;
    [SerializeField] private List<TextMeshProUGUI> textScore;

    private void Start()
    {
        LoadEntries();
    }

    public void LoadEntries()
    {
        Leaderboards.Score.GetEntries((entries) =>
        {
            int value = 1;

            for (int i = 0; i < 10; i++)
            {
                textRank[i].text = value.ToString();
                textName[i].text = "EMPTY";
                textScore[i].text = "0";

                value++;
            }

            int loopLength = (entries.Length < textName.Count) ? entries.Length : textName.Count;

            for (int i = 0; i < loopLength; i++)
            {
                textRank[i].text = $"{entries[i].Rank}";
                textName[i].text = $"{entries[i].Username}";
                textScore[i].text = $"{entries[i].Score}";
            }
        });
    }
}
