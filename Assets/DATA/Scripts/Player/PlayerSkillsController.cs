using System;
using System.Collections;
using UnityEngine;

namespace DATA.Scripts.Player
{
    public class PlayerSkillsController : MonoBehaviour
    {
        [SerializeField] private GameObject drone;
        [SerializeField] private float droneLifetime = 5f;
        private bool _toggle;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(!_toggle)
                    drone.SetActive(false);
                else
                {
                    drone.SetActive(true);
                    StartCoroutine(DeactiveSkill(drone, droneLifetime));
                }
                _toggle = !_toggle;
            }
        }

        IEnumerator DeactiveSkill(GameObject skill, float time)
        {
            yield return new WaitForSeconds(time);
            skill.SetActive(false);
            _toggle = false;
        }
    }
}
