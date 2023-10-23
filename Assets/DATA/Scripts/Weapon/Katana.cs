using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DATA.Scripts.Weapon
{
    public class Katana : MonoBehaviour
    {
        private Animator _anim;
        private AudioSource[] _audioSource;
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        public float delayBeforeAtk = 1f;
        public float delayBeforeSkill = 1f;
        public float skillRange = 50f;
        private bool _canAtk = true;
        private bool _canUseSkill = true;

        [SerializeField] public Transform player;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _audioSource = GetComponents<AudioSource>();
        }

        private void OnEnable()
        {
            _canAtk = true;
            _canUseSkill = true;
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

            if (Input.GetMouseButtonDown(1) && _canUseSkill)
            {
                UseSkill();
            }

        }

        private void UseSkill()
        {
            if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, skillRange))
            {


                player.transform.DOMove(hit.point - new Vector3(1, 0, 1), 0.25f);
                Debug.Log(hit.transform.name);

            }
            else
            {
                var transform1 = player.transform;
                player.transform.DOMove(transform1.position + transform1.forward * 50, 0.5f);
            }

            StartCoroutine(IEDelaySkill(delayBeforeSkill));
            _audioSource[1].Play();
            _audioSource[0].Play();
            _anim.ResetTrigger(Attack);
            _anim.SetTrigger(Attack);
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
