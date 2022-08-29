using System;
using System.Collections;
using OnTime.Audio;
using OnTime.Game;
using OnTime.Plattform;
using OnTime.UI;
using UnityEngine;

namespace OnTime.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float FALL_SPEED = 60f; 
        private Transform movement;
        private Transform splitter;
        private PlattformPositionController plattformPositionController;
        private PlayerPosition playerPosition;
        private GameStatusActivator gameStatusActivator;
        private float playerPlattformDistance;
        private protected float animationProcess;
        private bool isAnimating;
        private Vector3 animationPosition;
        private bool isGameOver;
        private ScoreCalcoulator scoreCalculator;
        private AudioManager audioManager;
        private SaveSystem saveSystem;

        private void Awake()
        {
            saveSystem = new SaveSystem();
            playerPosition = new PlayerPosition();
            movement = GetComponentInChildren<Transform>().Find("Movement");
            splitter = GetComponentInChildren<Transform>().Find("Splitter");
            plattformPositionController = FindObjectOfType<PlattformPositionController>();
            gameStatusActivator = FindObjectOfType<GameStatusActivator>();
            scoreCalculator = FindObjectOfType<ScoreCalcoulator>();
            audioManager = FindObjectOfType<AudioManager>();
            UpdateColorOfPlayer();
        }

        private void Update()
        {
            UpdateFallingPlayerDifference();
            if (isAnimating)
            {
                animationProcess += Time.deltaTime;
                movement.transform.position = MathParabola.Parabola(movement.transform.position,animationPosition, 2f,animationProcess / 0.3f);
            }
        }

        private void UpdateColorOfPlayer()
        {
            var colors = saveSystem.LoadPlayerData().colors;
            print(colors);
            Material color = (Material) Resources.Load(colors, typeof(Material));
            movement.GetComponent<Renderer>().material = color;
            splitter.GetComponent<Renderer>().material = color;
        }

        private void UpdateFallingPlayerDifference()
        {
            var position = Vector3.down * FALL_SPEED * Time.deltaTime;
            splitter.Translate(position, Space.World);
        }
        public void MovePlayer()
        {
            var currentPlattformAxis = plattformPositionController.GetPlattformAxis();
            var positionOfNextPlattform = plattformPositionController.GetMovingPlattformPosition();
            animationPosition = playerPosition.ForJump(movement.position, currentPlattformAxis, positionOfNextPlattform);
            isAnimating = true;
            StartCoroutine(JumpFinish(0.3f));
        }

        public void PlayerHalfing()
        {
            playerPlattformDistance = DistanceCalculator(plattformPositionController.GetPlattformAxis(), movement);
            var differenceBetweenPlattformPlayer = DifferenceBetweenPlayerAndPlattform(plattformPositionController.GetPlattformAxis(), playerPlattformDistance, movement);
            LookIfMovementDied(differenceBetweenPlattformPlayer, plattformPositionController.GetPlattformAxis());
            if (!isGameOver)
            {
                movement.localScale = PlayerRescale(plattformPositionController.GetPlattformAxis(), movement, differenceBetweenPlattformPlayer);
                movement.position = playerPosition.AfterJump(plattformPositionController.GetPlattformAxis(),movement,playerPlattformDistance,differenceBetweenPlattformPlayer);
                SetPlayerFallingDifference(differenceBetweenPlattformPlayer, playerPlattformDistance, movement);
                scoreCalculator.AddPoints();
            }
        }

        private void LookIfMovementDied (float difference, PlattformAxis plattformAxis)
        {
            var isMovmentOverXAxis = plattformAxis.Equals(PlattformAxis.X) && difference > movement.localScale.x;
            var isMovmentOverZAxis = plattformAxis.Equals(PlattformAxis.Z) && difference > movement.localScale.z;
            if ( isMovmentOverXAxis || isMovmentOverZAxis)
            {
                gameStatusActivator.EndGame();
                audioManager.Play("fall");
                splitter.localScale = movement.localScale;
                splitter.position = movement.position;
                movement.localScale = Vector3.zero;
                isGameOver = true;
            }
        }

        private void SetPlayerFallingDifference(float difference, float distance, Transform movement)
        {
            var isSplitterGreaterZero = difference > 0;
            if (plattformPositionController.GetPlattformAxis().Equals(PlattformAxis.X) && isSplitterGreaterZero)
            {
                splitter.localScale = new Vector3(difference, movement.localScale.y, movement.localScale.z);
                splitter.position = new Vector3(OperatorFallingDistance(distance, movement.position.x, (movement.localScale.x + difference) / 2), movement.position.y, movement.position.z);
                return;
            }
            if (plattformPositionController.GetPlattformAxis().Equals(PlattformAxis.Z) && isSplitterGreaterZero)
            {
                splitter.localScale = new Vector3(movement.localScale.x, movement.localScale.y, difference);
                splitter.position = new Vector3(movement.position.x , movement.position.y, OperatorFallingDistance(distance, movement.position.z, (movement.localScale.z + difference) / 2));
                return;
            }
            splitter.localScale = Vector3.zero;
        }

        private float OperatorFallingDistance(float distance, float playerPositionAxis, float playerLocalScaleDifference)
        {
            return distance < 0 ? (playerPositionAxis - playerLocalScaleDifference) : (playerPositionAxis + playerLocalScaleDifference);
        }
        
        private float DistanceCalculator(PlattformAxis plattformAxis, Transform player)
        {
            if (plattformAxis.Equals(PlattformAxis.X))
            {
                return player.position.x - plattformPositionController.GetStoppedPlattform().position.x;
            }
            else
            {
                return player.position.z - plattformPositionController.GetStoppedPlattform().position.z;
            }
        }

        private Vector3 PlayerRescale(PlattformAxis plattformAxis, Transform player, float differencePlayerPlattform)
        {
            var rescaling = player.transform.localScale;
            var isPlayerSame = differencePlayerPlattform < 0;
            if (plattformAxis.Equals(PlattformAxis.X) && differencePlayerPlattform < rescaling.x && !isPlayerSame)
            { 
                 rescaling.x = player.localScale.x - differencePlayerPlattform;
                 return rescaling;
            }
            if (plattformAxis.Equals(PlattformAxis.Z) && differencePlayerPlattform < rescaling.z && !isPlayerSame)
            {
                rescaling.z = player.localScale.z - differencePlayerPlattform;
                return rescaling;
            }
            return rescaling;
        }

        private float DifferenceBetweenPlayerAndPlattform(PlattformAxis plattformAxis, float distance, Transform player)
        {
            switch (plattformAxis)
            {
                case PlattformAxis.X:
                    return (Math.Abs(playerPlattformDistance) - ((20 - player.localScale.x) / 2));

                case PlattformAxis.Z:
                    return (Math.Abs(playerPlattformDistance) - ((20 - player.localScale.z) / 2));
                default:
                    return 0f;
            }
        }
        private IEnumerator JumpFinish(float time)
        {
            yield return new WaitForSeconds(time);
            isAnimating = false;
            animationProcess = 0;
            movement.position = new Vector3(movement.position.x, 7.5f, movement.position.z);
            PlayerHalfing();
        }
    }
}