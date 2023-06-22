using UnityEngine;

namespace Project.Scripts.Audio
{
    public class AudioPlayerCommunicator : MonoBehaviour
    {
        public void PlayEffect(AudioClip clip)
        {
            if (!AudioPlayer.Instance) return;
            AudioPlayer.Instance.PlayEffect(clip);
        }

        public void PlayRandomEffect(AudioCollection collection)
        {
            var clips = collection.AudioClips;
            var audioClip = clips[Random.Range(0, clips.Length)];
            PlayEffect(audioClip);
        }
    }
}