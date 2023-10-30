using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DATA.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] GameObject nextScene;
        [SerializeField] Slider slider;
        [SerializeField] float speed = 0.5f;
        float _time;

        void Update()
        {
            _time += Time.deltaTime * speed;
            slider.value = _time;

            if (_time > 1)
            {
                _time = 0;
                LoadScene();
            }
        }
        
        private void LoadScene()
        {
            nextScene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
