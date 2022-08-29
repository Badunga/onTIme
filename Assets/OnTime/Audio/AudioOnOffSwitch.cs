using TMPro;
using UnityEngine;

namespace OnTime.Audio
{
    public class AudioOnOffSwitch : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI soundButton;
    
        [SerializeField] private TextMeshProUGUI musicButton;
        void Start()
        {
            LookIfSoundMuted();
            LookIfMusicMuted();
        }
    
        public void ChangeSoundMuteState()
        {
            var isSoundMuted = PlayerPrefs.GetInt("isSoundMuted");
            if (isSoundMuted == 0)
            {
                PlayerPrefs.SetInt("isSoundMuted",1); 
                soundButton.text = "SOUND: OFF";
            }
            else
            {
                PlayerPrefs.SetInt("isSoundMuted",0); 
                soundButton.text = "SOUND: ON";
            }
        }
        
        public void ChangeMusicMuteState()
        {
            var isSoundMuted = PlayerPrefs.GetInt("isMusicMuted");
            if (isSoundMuted == 0)
            {
                PlayerPrefs.SetInt("isMusicMuted",1); 
                musicButton.text = "MUSIC: OFF";
            }
            else
            {
                PlayerPrefs.SetInt("isMusicMuted",0); 
                musicButton.text = "MUSIC: ON";
            }
        }
    
        
        private void LookIfSoundMuted()
        {
            if (PlayerPrefs.GetInt("isSoundMuted") != 1 )
            {
                PlayerPrefs.SetInt("isSoundMuted", 0);
                soundButton.text = "SOUND: ON";
            }
            else
            {
                PlayerPrefs.SetInt("isSoundMuted", 1);
                soundButton.text = "SOUND: OFF";
            }
        }
        
        private void LookIfMusicMuted()
        {
            if (PlayerPrefs.GetInt("isMusicMuted") != 1 )
            {
                PlayerPrefs.SetInt("isMusicMuted", 0);
                musicButton.text = "MUSIC: ON";
            }
            else
            {
                PlayerPrefs.SetInt("isMusicMuted", 1);
                musicButton.text = "MUSIC: OFF";
            }
        }
    }
}