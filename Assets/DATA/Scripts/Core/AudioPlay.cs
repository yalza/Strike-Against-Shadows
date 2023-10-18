using System;
using UnityEngine;

namespace DATA.Scripts.Core
{
    public class AudioPlay : MonoBehaviour
    {
        AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.Play();
        }
    }
}