using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace OnTime.Game
{
    public class GooglePlayStart : MonoBehaviour
    {
        private void Awake()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            SignInToGooglePLayServices();
        }

        private void SignInToGooglePLayServices()
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                if (result.Equals(SignInStatus.Success))
                {
                    Debug.Log("Success");
                }
            });
        }
        
        public void ShowScoreBoard()
        {
            Social.Active.ShowLeaderboardUI();
        }
    }
}