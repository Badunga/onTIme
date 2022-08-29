using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OnTime.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void OpenLeaderBoard()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}