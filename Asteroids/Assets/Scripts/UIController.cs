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
    List<Image> iconList = new List<Image>();
    private int playerLivesLeft = 3;
    private int score = 0;

    private void Start()
    {
        iconList.Add(lives1);
        iconList.Add(lives2);
        iconList.Add(lives3);

        RenderScore();
        PlayerHit();
        PlayerHit();
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
        playerLivesLeft--;

        if (iconList.Count >= 0)
        {
            iconList.RemoveAt(playerLivesLeft);
        }
    }

    private void RenderScore()
    {
        ScoreValue.text = score.ToString();
    }
}
