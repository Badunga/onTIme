using System;
using UnityEngine;

namespace OnTime.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] 
        private Sound[] sounds;
        private void Awake()
        {
            foreach (Sound sound in sounds)
            {
                sound.source =  gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
        }

        private void Start()
        {
            if (PlayerPrefs.GetInt("isMusicMuted") == 0)
            {
                Play("music");
            }
        }

        public void Play(string name)
        {
            if (PlayerPrefs.GetInt("isSoundMuted") == 0)
            {
                Sound sound =  Array.Find(sounds, sound => sound.name.Equals(name));
                sound.source.Play();
            }
        }
    }
}
