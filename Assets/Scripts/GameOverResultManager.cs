using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverResultManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI detailText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + GameResultData.score.ToString();
        rankText.text = "RANK: " + GameResultData.rank;
        detailText.text = $"Perfect: {GameResultData.perfectCount}\nGreat: {GameResultData.greatCount}\nMiss: {GameResultData.missCount}";
    }

    public void BackToMenu()
    {
        GameResultData.Reset();
        SceneManager.LoadScene("StartMenuScene");
    }

    public void PlayAgain()
    {
        GameResultData.Reset();
        SceneManager.LoadScene("MusicPlayScene");
    }

    public void Next()
    {
        GameResultData.Reset();
        SceneManager.LoadScene("SelectTrackScene");
    }
}
