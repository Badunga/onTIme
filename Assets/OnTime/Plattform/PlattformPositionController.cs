using OnTime.Game;
using OnTime.UI;
using UnityEngine;

namespace OnTime.Plattform
{
    public class PlattformPositionController : MonoBehaviour
    {
        private const float FALL_SPEED = 40f; 
        private Transform plattformXAxis;
        private Transform plattformZAxis;
        private MovementCalculator movementCalculator;
        private StartPositionCalculator startPositionCalculator;
        private Transform fallingPlattform;
        private Vector3 left;
        private Vector3 right;
        private PlattformAxis plattformAxis;
        private float plattformPosition;
        private Transform currentStoppedPlattform;
        private NextPositionCalculator nextPositionCalculator;
        private GameStatusActivator gameStatusActivator;
        private ScoreCalcoulator scoreCalcoulator;
        private float speed;
        
        private void Awake()
        {
            gameStatusActivator = FindObjectOfType<GameStatusActivator>();
            scoreCalcoulator = FindObjectOfType<ScoreCalcoulator>();
            nextPositionCalculator = new NextPositionCalculator();
            movementCalculator = new MovementCalculator();
            startPositionCalculator = new StartPositionCalculator();
            plattformZAxis = GameObject.Find("PlattformZ").GetComponent<Transform>();
            plattformXAxis = GameObject.Find("PlattformX").GetComponent<Transform>();
            fallingPlattform = GameObject.Find("PlattformFall").GetComponent<Transform>();
        }
        
        private void Start()
        {
            speed = 1f;
            plattformAxis = PlattformAxis.Z;
            left = startPositionCalculator.OnLeftSide(plattformZAxis);
            right = startPositionCalculator.OnRightSide(plattformZAxis);
        }

        private void Update()
        {
            
            UpdateFallingPlattform();
            if (gameStatusActivator.IsPlaying())
            { 
                UpdateSpeedMovement();
            }
        }

        private void UpdateSpeedMovement()
        {
            if (plattformAxis.Equals(PlattformAxis.Z))
            {
                plattformZAxis.position = movementCalculator.GenerateNextMovement(left, right, speed);
            }
            else
            {
                plattformXAxis.position = movementCalculator.GenerateNextMovement(left, right, speed);
            }
        }

        private void UpdateFallingPlattform()
        {
            var position = Vector3.down * FALL_SPEED * Time.deltaTime;
            fallingPlattform.transform.Translate(position, Space.World);
        }
        
        public void ChangeAxis()
        {
            if (plattformAxis.Equals(PlattformAxis.Z))
            {
                fallingPlattform.position = plattformXAxis.position;
                plattformPosition = plattformZAxis.position.z;
                plattformXAxis.position = nextPositionCalculator.NextPositionOfX(plattformZAxis);
                left = startPositionCalculator.EndOfDown(plattformXAxis);
                right = startPositionCalculator.EndOfUp(plattformXAxis);
                plattformAxis = PlattformAxis.X;
                currentStoppedPlattform = plattformZAxis;
            }
            else
            {
                fallingPlattform.position = plattformZAxis.position;
                plattformPosition = plattformXAxis.position.x;
                plattformZAxis.position = nextPositionCalculator.NextPositionOfZ(plattformXAxis);
                left = startPositionCalculator.OnLeftSide(plattformZAxis);
                right = startPositionCalculator.OnRightSide(plattformZAxis);
                plattformAxis = PlattformAxis.Z;
                currentStoppedPlattform = plattformXAxis;
            }
        }

        public void SpeedUp()
        {
            var score = scoreCalcoulator.GetScore();
            if ((score % 5) -4 == 0 && score != 0)
            {
                speed += 0.5f;
            }
        }

        public float GetMovingPlattformPosition()
        {
            return plattformPosition;
        }

        public PlattformAxis GetPlattformAxis()
        {
            return plattformAxis;
        }

        public Transform GetStoppedPlattform()
        {
            return currentStoppedPlattform;
        }
    }
}


