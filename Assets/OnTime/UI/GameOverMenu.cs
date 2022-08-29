using TMPro;
using UnityEngine;

namespace OnTime.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI totalScore;
        [SerializeField] 
        private TextMeshProUGUI newCoins;
        private ScoreCalcoulator scoreCalcoulator;
        private PlayerCoins playerCoins;

        private void Awake()
        {
            scoreCalcoulator = FindObjectOfType<ScoreCalcoulator>();
            playerCoins = FindObjectOfType<PlayerCoins>();
        }

        private void OnEnable()
        {
            var score = scoreCalcoulator.GetScore();
            newCoins.text = "+" + playerCoins.GetCalculatedCoinsAfterGameOver(score).ToString();
            totalScore.text = score.ToString();
        }
    }
}