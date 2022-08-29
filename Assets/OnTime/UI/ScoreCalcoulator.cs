using OnTime.Plattform;
using TMPro;
using UnityEngine;

namespace OnTime.UI
{
    public class ScoreCalcoulator : MonoBehaviour
    {
        private TextMeshProUGUI gameOverScore;
        private PlattformPositionController plattformPositionController;
        private int value;
        private TextMeshProUGUI score;

        private void Awake()
        {
            plattformPositionController = FindObjectOfType<PlattformPositionController>();
            score = GetComponent<TextMeshProUGUI>();
        }

        public void AddPoints()
        {
            value++;
            score.text = value.ToString();
        }

        public int GetScore()
        {
            return value;
        }
    }
}
