using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DATA.Scripts.Weapon
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public GameObject[] weapons;
        private int _currentWeapon;
        private Animator _animator;
        public LayerMask layerMask;
        private static readonly int Switch = Animator.StringToHash("Switch");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpWeapon();
            }
        }

        private void NextIndexWeapon()
        {

            _animator.SetTrigger(Switch);
            GameObject a = weapons[_currentWeapon];
            _currentWeapon++;
            if (_currentWeapon >= weapons.Length)
            {
                _currentWeapon = 0;
            }
            GameObject b = weapons[_currentWeapon];
            StartCoroutine(IEDelaySwitch(0.5f / 5f, a, b));


        }

        private void PrevIndexWeapon()
        {
            _animator.SetTrigger(Switch);
            GameObject a = weapons[_currentWeapon];
            _currentWeapon--;
            if (_currentWeapon < 0)
            {
                _currentWeapon = weapons.Length - 1;
            }
            GameObject b = weapons[_currentWeapon];
            StartCoroutine(IEDelaySwitch(0.1f, a, b));
        }
        
        IEnumerator IEDelaySwitch(float time,GameObject a, GameObject b)
        {
            yield return new WaitForSeconds(time);
            a.SetActive(false);
            b.SetActive(true);
        }
        
        private void PickUpWeapon()
        {
            
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/ 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hit, 100,layerMask))
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
    
}
