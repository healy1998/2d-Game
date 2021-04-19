using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Stephen.Scoreboards;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Scoreboard scoreboard;
    public Text pointsText;
    public InputField name;
    public void Setup(int score)
    {
        score = ScoreScript.ScoreValue;
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void SubmitButton()
    {
        scoreboard.AddEntry(new ScoreboardEntryData()
        {
            entryName = name.text.ToString(),
            entryScore = ScoreScript.ScoreValue
        });
        ScoreScript.ScoreValue = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 1");
        ScoreScript.ScoreValue = 0;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        ScoreScript.ScoreValue = 0;
    }
}