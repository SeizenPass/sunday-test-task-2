using System;
using UnityEngine;

namespace Project.Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;

        public static AudioPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void PlayEffect(AudioClip audioTrack)
        {
            if (!audioTrack) return;
            sfxSource.PlayOneShot(audioTrack);
        }
    }
}