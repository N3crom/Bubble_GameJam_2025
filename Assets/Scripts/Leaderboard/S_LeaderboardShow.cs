using System.Collections.Generic;
using UnityEngine;

using Dan.Main;
using TMPro;

public class S_LeaderboardShow : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> textRank;
    [SerializeField] private List<TextMeshProUGUI> textName;
    [SerializeField] private List<TextMeshProUGUI> textWave;

    public GameObject scroll;

    private void Start()
    {
        LoadEntries();
    }

    public void LoadEntries()
    {
        Leaderboards.Score.GetEntries((entries) =>
        {
            int value = 1;

            foreach (var t in textRank)
            {
                t.text = value.ToString() + ".";
                value++;
            }

            foreach (var t in textName)
            {
                t.text = "EMPTY";
            }

            foreach (var t in textWave)
            {
                t.text = "0";
            }

            int loopLength = (entries.Length < textName.Count) ? entries.Length : textName.Count;
            for (int i = 0; i < loopLength; i++)
            {
                bool isMine = entries[i].IsMine();

                if(isMine)
                {
                    textRank[i].color = Color.green;
                    textName[i].color = Color.green;
                    textWave[i].color = Color.green;
                }

                textRank[i].text = $"{entries[i].Rank}" + ".";
                textName[i].text = $"{entries[i].Username}";
                textWave[i].text = $"{entries[i].Score}";
            }
        });
    }
}
