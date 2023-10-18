using UnityEngine;

namespace DATA.Scripts.Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        public void PlaySound(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}