using System;
using UnityEngine;
using DG.Tweening;

namespace DATA.Scripts.Weapon
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public GameObject[] weapons;
        private int _currentWeapon;
        readonly Vector3 _position = new Vector3(0,-1,0);

        private void Start()
        {
            weapons[_currentWeapon].SetActive(true);
        }

        private void Update()
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
                NextIndexWeapon();
            if(Input.GetAxis("Mouse ScrollWheel") < 0f)
                PrevIndexWeapon();
        }

        private void NextIndexWeapon()
        {

            weapons[_currentWeapon].SetActive(false);
            _currentWeapon++;
            if (_currentWeapon >= weapons.Length)
            {
                _currentWeapon = 0;
            }
            weapons[_currentWeapon].SetActive(true);

            
            
        }

        private void PrevIndexWeapon()
        {
            weapons[_currentWeapon].SetActive(false);
            _currentWeapon--;
            if (_currentWeapon < 0)
            {
                _currentWeapon = weapons.Length - 1;
            }
            weapons[_currentWeapon].SetActive(true);
            
        }
    }
    
}
