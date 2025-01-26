using System.Collections.Generic;
using UnityEngine;

using Dan.Main;
using TMPro;

public class S_LeaderboardShow : MonoBehaviour
{
    [SerializeField] private List<string> textRank;
    [SerializeField] private List<string> textName;
    [SerializeField] private List<string> textScore;

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
                textRank[i] = value.ToString();
                textName[i] = "EMPTY";
                textScore[i] = "0";

                value++;
            }

            int loopLength = (entries.Length < textName.Count) ? entries.Length : textName.Count;

            for (int i = 0; i < loopLength; i++)
            {
                textRank[i] = $"{entries[i].Rank}";
                textName[i] = $"{entries[i].Username}";
                textScore[i] = $"{entries[i].Score}";

                /*bool isMine = entries[i].IsMine();

                if(isMine)
                {
                    textRank[i].color = Color.green;
                    textName[i].color = Color.green;
                    textScore[i].color = Color.green;
                }

                textRank[i].text = $"{entries[i].Rank}" + ".";
                textName[i].text = $"{entries[i].Username}";
                textScore[i].text = $"{entries[i].Score}";*/
            }
        });
    }
}
