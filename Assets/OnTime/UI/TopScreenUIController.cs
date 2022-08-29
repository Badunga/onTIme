using UnityEngine;

namespace OnTime.UI
{
    public class TopScreenUIController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject pausebutton;
        [SerializeField] 
        private GameObject scoreCounter;

        private void Start()
        {
            pausebutton.SetActive(true);
            scoreCounter.SetActive(true);
        }

        public void DeactivateTopScreenUI()
        {
            pausebutton.SetActive(false);
            scoreCounter.SetActive(false);
        }
    }
}