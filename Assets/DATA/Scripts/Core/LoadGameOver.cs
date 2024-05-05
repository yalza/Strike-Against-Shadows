using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DATA.Scripts.Core
{
    public class LoadGameOver : MonoBehaviour
    {
        [SerializeField] private string levelName;
        [SerializeField] TextMeshProUGUI scoreText;
        private void Start()
        {
            scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
            Invoke(nameof(LoadScene), 5);
        }
        
        public void LoadScene()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}