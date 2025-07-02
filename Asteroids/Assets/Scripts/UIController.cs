using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreValue;
    [SerializeField] Image lives1;
    [SerializeField] Image lives2;
    [SerializeField] Image lives3;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] Button retryButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] GameObject newHighScoreText;
    [SerializeField] TextMeshProUGUI accuracyValue;
    public Player playerScript;
    private List<Image> iconList = new List<Image>();
    private int playerLivesLeft = 3;
    private int score = 0;
    private int shotsHit = 0;
    private int highestScore = 0;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        iconList.Add(lives1);
        iconList.Add(lives2);
        iconList.Add(lives3);

        RenderScore();
        RenderLifeIcons();   
    }

    public void RenderLifeIcons()
    {
        ResetIcons();

        for (int i = 0; i < iconList.Count; i++)
        {
            iconList[i].gameObject.SetActive(true);
        }
    }

    private void ResetIcons()
    {
        lives1.gameObject.SetActive(false);
        lives2.gameObject.SetActive(false);
        lives3.gameObject.SetActive(false);
    }

    public void PlayerHit()
    {
        if (playerLivesLeft >= 0)
        {
            playerLivesLeft--;
        }
        
        if (playerLivesLeft == 0)
        {
            GameOver();
        }

        //Add GameOver function for lives hitting 0

        if (iconList.Count > 0)
        {
            iconList.RemoveAt(playerLivesLeft);
            RenderLifeIcons();
        }
    }

    private void GameOver()
    {
        gameOverContainer.SetActive(true);
        if (playerScript.GetShotsFired() == 0)
        {
            accuracyValue.text = "Accuracy: 0%";
        }
        else
        {
            accuracyValue.text = "Accuracy: " + ((float)shotsHit / playerScript.GetShotsFired() * 100f).ToString("F1") + "%";
        }
        Debug.Log(score);

        if (score > highestScore)
        {
            newHighScoreText.SetActive(true);
            highestScore = score;
            Debug.Log("New High Score!");
        }

        playerScript.GameOver();
    }

    public void RetryGame()
    {
        newHighScoreText.SetActive(false);

        //Reset score to 0
        score = 0;
        RenderScore();

        //Add back the 3 life icons
        playerLivesLeft = 3;
        iconList.Add(lives1);
        iconList.Add(lives2);
        iconList.Add(lives3);
        RenderLifeIcons();

        //Disable the game over UI
        gameOverContainer.SetActive(false);

        //Call the Restart Game script to respawn the player
        playerScript.RestartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void IncreaseScore(int points)
    {
        score += points;
        RenderScore();
    }

    private void RenderScore()
    {
        ScoreValue.text = score.ToString();
    }

    public void IncreaseShotsHit()
    {
        shotsHit++;
    }
}
