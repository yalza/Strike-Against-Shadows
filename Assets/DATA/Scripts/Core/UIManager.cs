using System;
using System.Collections.Generic;
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

        private void Start()
        {
            Observer.Instant.RegisterObserver(Constant.updateHpSlider, UpdateHpSlider);
        }

        private void UpdateHpSlider(List<object> param)
        {
            if (param.Count == 2)
            {
                hpSlider.value = (float) param[0] / (float) param[1];
                hpText.text = param[0].ToString();
                maxHpText.text = param[1].ToString();
            }
        }
        
    }
}