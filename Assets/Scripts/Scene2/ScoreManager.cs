using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        FishDetect.OnPoissonCollected += UpdateScore;
    }

    private void OnDisable()
    {
        FishDetect.OnPoissonCollected -= UpdateScore;
    }


    public void UpdateScore(Poison enemyRef)
    {
        score += enemyRef.scorewhenKilled;
        scoreText.text = score.ToString();
    }
}
