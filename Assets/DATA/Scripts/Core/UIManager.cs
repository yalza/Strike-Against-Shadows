using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DATA.Scripts.Core
{
    public class UIManager : MonoBehaviour
    {
        [Header("Hp")]
        [SerializeField] private Slider hpSlider;
        [SerializeField] private TextMeshProUGUI hpText,maxHpText;

        [Header("Ammo")] [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI maxAmmoText;
        
        [Header("Score")] [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timeText;

        private void Awake()
        {
            Observer.Instant.RegisterObserver(Constant.updateHpSlider, UpdateHpSlider);
            Observer.Instant.RegisterObserver(Constant.updateTimeText, UpdateTimeText);
            
        }

        private void Start()
        {
            
            Observer.Instant.RegisterObserver(Constant.updateAmmoText, UpdateAmmoText);
            Observer.Instant.RegisterObserver(Constant.updateScoreText, UpdateScoreText);
            
        }

        private void UpdateHpSlider(object param)
        {
            if (param is (float, float))
            {
                var (health, maxHealth) = ((float, float)) param;
                hpSlider.value = health / maxHealth ;
                
                hpText.text = health.ToString(CultureInfo.InvariantCulture);
                maxHpText.text = maxHealth.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        private void UpdateAmmoText(object param)
        {
            if (param is (int, int))
            {
                var (ammo, maxAmmo) = ((int, int)) param;
                ammoText.text = ammo.ToString();
                maxAmmoText.text = maxAmmo.ToString();
            }
        }
        
        private void UpdateScoreText(object param)
        {
            if (param is int)
            {
                var score = (int) param;
                scoreText.text ="Score : " + score.ToString();
            }
        }
        
        private void UpdateTimeText(object param)
        {
            if (param is int)
            {
                var time = (int) param;
                int a = time / 60;
                int b = time % 60;
                string c, d;
                if (a < 10)
                {
                    c = "0" + a;
                }
                else
                {
                    c = a.ToString();
                }
                if (b < 10)
                {
                    d = "0" + b;
                }
                else
                {
                    d = b.ToString();
                }

                timeText.text = "Time : " + c + ":" + d;
            }
        }
        
    }
}