using UnityEngine;

namespace Project.Scripts.Audio
{
    [CreateAssetMenu( menuName = "Project/Audio Collection")]
    public class AudioCollection : ScriptableObject
    {
        [SerializeField] private AudioClip[] audioClips;

        public AudioClip[] AudioClips => audioClips;
    }
}