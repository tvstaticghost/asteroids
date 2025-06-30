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
    public Player playerScript;
    private List<Image> iconList = new List<Image>();
    private int playerLivesLeft = 3;
    private int score = 0;

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
        playerScript.GameOver();
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
}
