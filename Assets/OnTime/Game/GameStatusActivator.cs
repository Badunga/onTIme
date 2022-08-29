using OnTime.Audio;
using OnTime.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OnTime.Game
{
    public class GameStatusActivator : MonoBehaviour
    {
        private GooglePlayManger googlePlayManger;
        private GameStatus gameStatus;
        private TopScreenUIController topScreenUIController;
        private PlayerCoins playerCoins;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject pauseMenu;
        
        private void Awake()
        {
            gameStatus = GameStatus.Playing;
            Time.timeScale = 1f;
            playerCoins = FindObjectOfType<PlayerCoins>();
            topScreenUIController = FindObjectOfType<TopScreenUIController>();
            googlePlayManger = FindObjectOfType<GooglePlayManger>();
        }
        
        public void Pause()
        {
            gameStatus = GameStatus.Paused;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    
        public void EndGame()
        {
            gameOverMenu.SetActive(true);
            gameStatus = GameStatus.Over;
            googlePlayManger.UpdateScore();
            topScreenUIController.DeactivateTopScreenUI();
            playerCoins.SaveCoinsAfterGameover();
        }

        public void Restart()
        {
            gameStatus = GameStatus.Playing;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
        public void Resume()
        {
            Time.timeScale = 1f;
            gameStatus = GameStatus.Playing;
            pauseMenu.SetActive(false);
        }
    
        public void MainMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        } 
    
        public bool IsPlaying()
        {
            return gameStatus.Equals(GameStatus.Playing);
        }
    }
}
