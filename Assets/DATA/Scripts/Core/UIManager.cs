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

        private void Awake()
        {
            Observer.Instant.RegisterObserver(Constant.updateHpSlider, UpdateHpSlider);
        }

        private void Start()
        {
            
            Observer.Instant.RegisterObserver(Constant.updateAmmoText, UpdateAmmoText);
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
        
        
    }
}