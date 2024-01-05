using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> rank;
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    public GameObject storyButton;
    public GameObject endlessButton;

    private string plk = "c95c898060bf4ce9f5c3befefa552d0ccd7fb17d3c40d0b61e6809308b69b130";

    private void Start()
    {
        GetLeaderboard();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Mode"))
        {
            string mode = PlayerPrefs.GetString("Mode");
            if (mode == "Story")
            {
                storyButton.SetActive(true);
                endlessButton.SetActive(false);
            }
            else if (mode == "Endless")
            {
                storyButton.SetActive(false);
                endlessButton.SetActive(true);
            }
        }
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(plk, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for(int i = 0; i < loopLength; i++)
            {
                rank[i].text = msg[i].Rank.ToString();
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLoaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(plk, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
