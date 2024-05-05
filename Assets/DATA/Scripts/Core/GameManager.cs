using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DATA.Scripts.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int time = 200;
        private int _score;
        
        private void Start()
        {
            InvokeRepeating(nameof(UpdateTime),0,1);
        }
        
        public void UpdateTime()
        {
            Observer.Instant.NotifyObservers(Constant.updateTimeText, this.time);
            this.time--;
            if (this.time <= 0)
            {
                PlayerPrefs.SetInt("Score", _score);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOver");
            }
        }
        
        public void UpdateScore(int score)
        {
            _score += score;
            Observer.Instant.NotifyObservers(Constant.updateScoreText, _score);
        }
        
        
    }
}