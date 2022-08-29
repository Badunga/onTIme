using OnTime.Audio;
using OnTime.Game;
using OnTime.Plattform;
using OnTime.Player;
using UnityEngine;

namespace OnTime.Click
{
    public class ClickController : MonoBehaviour
    {
        private PlattformPositionController plattformPositionController;
        private PlayerController playerController;
        private GameStatusActivator gameStatusActivator;
        private AudioManager audioManager;

        private void Awake()
        {
            plattformPositionController = FindObjectOfType<PlattformPositionController>();
            playerController = FindObjectOfType<PlayerController>();
            gameStatusActivator = FindObjectOfType<GameStatusActivator>();
            audioManager = FindObjectOfType<AudioManager>();
        }
        
        private void Update()
        {
            var isValidPlayerInput = Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.width * 1.67f;
            if (isValidPlayerInput && gameStatusActivator.IsPlaying())
            {
                plattformPositionController.SpeedUp();
                plattformPositionController.ChangeAxis();
                playerController.MovePlayer();
                audioManager.Play("jump");
            }
        }
    }
}