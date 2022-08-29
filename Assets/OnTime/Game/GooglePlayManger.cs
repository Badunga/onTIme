using OnTime.UI;
using UnityEngine;

namespace OnTime.Audio
{
    public class GooglePlayManger : MonoBehaviour
    {
        private bool isConnectedToGooglePlay;
        private ScoreCalcoulator scoreCalcoulator;
    
        private void Awake()
        {
            scoreCalcoulator = FindObjectOfType<ScoreCalcoulator>();
        }
    
        public void UpdateScore()
        {
            var score = scoreCalcoulator.GetScore();
            Social.ReportScore(score,GPGSIds.leaderboard_leaderboard, (bool success) => {
                Debug.Log("success");
            });
        }
    }
}
