using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace DATA.Scripts.Weapon
{
    public class Katana : MonoBehaviour
    {
        private Animator _anim;
        private AudioSource[] _audioSource;
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        public float delayBeforeAtk = 1f;
        public float delayBeforeSkill = 1f;
        private bool _canAtk = true;
        private bool _canUseSkill = true;

        [SerializeField] private Transform player;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _audioSource = GetComponents<AudioSource>();
        }

        void Start()
        {
            
        }

        private void OnEnable()
        {
            _audioSource[0].Stop();
            _audioSource[1].Stop();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canAtk)
            {
                StartCoroutine(IEDelayAttack(delayBeforeAtk));
                _audioSource[0].Play();
                _anim.ResetTrigger(Attack);
                _anim.SetTrigger(Attack);
            }
            if(Input.GetMouseButtonDown(1) && _canUseSkill)
            {
                StartCoroutine(IEDelaySkill(delayBeforeSkill));
                player.transform.DOMove(player.position + player.forward * 30, 0.5f);
                _audioSource[1].Play();
                _audioSource[0].Play();
                _anim.ResetTrigger(Attack);
                _anim.SetTrigger(Attack);
            }
            
        }

        IEnumerator IEDelayAttack(float time)
        {
            _canAtk = false;
            yield return new WaitForSeconds(time);
            _canAtk = true;
        }
        
        IEnumerator IEDelaySkill(float time)
        {
            _canUseSkill = false;
            yield return new WaitForSeconds(time);
            _canUseSkill = true;
        }
        
        
    }
}
