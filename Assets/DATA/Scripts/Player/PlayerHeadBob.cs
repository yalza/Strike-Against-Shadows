using System;
using UnityEngine;

namespace DATA.Scripts.Player
{
    public class PlayerHeadBob : MonoBehaviour
    {
        [SerializeField] private Transform head;

        #region Head bob movement
        [Header("Head Bob Movement")]
        [SerializeField] private float headBobFrequency = 1.5f;                                                         // Tần số head bob 
        [SerializeField] private float headBocHeight = 0.3f;                                                            // Chiều cao head bob
        [SerializeField] private float headBobSwayAngle = 0.5f;                                                         // Góc xoay của head bob
        [SerializeField] private float headBobSideMovement = 0.05f;                                                     // Độ dịch chuyển sang ngang của head bob
        [SerializeField] private float bobHeightSpeedMultiplier = 0.3f;                                                 // Hệ số nhân tốc độ head bob theo chiều cao.
        [SerializeField] private float bobStrideSpeedLengthen = 0.3f;                                                   // Hệ số nhân tốc độ head bob theo chiều dài bước chân.

        #endregion


        #region Head bob jump
        [Header("Head Bob Jump")]
        [SerializeField] private float jumpLandMove = 3f;                                                               // Độ dịch chuyển của head bob khi nhảy
        [SerializeField] private float jumpLandTilt = 60f;                                                              // Góc xoay của head bob khi nhảy
        #endregion


        #region Audio clip
        [Header("Audio Clip")]
        [SerializeField] AudioClip[] footStepSounds;
        [SerializeField] AudioClip jumpSound;
        [SerializeField] AudioClip landSound;
        #endregion
        
        Rigidbody _rigidbody;
        AudioSource _audioSource;
        PlayerMovement _playerMovement;
        Vector3 _originalLocalPosition;
        
        float _headBobCycle;
        float _headBobFade;

        #region Spring caculation
        [Header("Spring Caculation")]
        float _springPosition;                                                                                          // Vị trí của lò xo     
        float _springVelocity;                                                                                          // Vận tốc của lò xo  
        float _springElastic = 1.1f;                                                                                    // Độ đàn hồi của lò xo
        float _springDampen = 0.8f;                                                                                     // Độ giảm sóc của lò xo
        float _springVelocityThreshold = 0.05f;                                                                        // Ngưỡng vận tốc của lò xo
        float _springPositionThreshold = 0.05f;                                                                       // Ngưỡng vị trí của lò xo
        
        #endregion
        
        Vector3 _prevPosition;                                                                                        // Vị trí trước đó của người chơi
        Vector3 _prevVelocity = Vector3.zero;                                                                      // Vận tốc trước đó của người chơi

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _originalLocalPosition = head.localPosition;
            _prevPosition = _rigidbody.position;
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            Vector3 velocity = (position - _prevPosition) / Time.deltaTime;
            Vector3 velocityChange = velocity - _prevVelocity;
            _prevPosition = position;
            _prevVelocity = velocity;

            _springVelocity -= velocityChange.y;
            _springVelocity -= _springPosition * _springElastic;
            _springVelocity *= _springDampen;
            _springPosition += _springVelocity * Time.deltaTime;
            _springPosition = Mathf.Clamp(_springPosition, -0.3f, 0.3f);
            
            if(Mathf.Abs(_springVelocity) < _springVelocityThreshold && Mathf.Abs(_springPosition) < _springPositionThreshold)
            {
                _springVelocity = 0f;
                _springPosition = 0f;
            }
            
            float flatVelocity = new Vector3(velocity.x, 0f, velocity.z).magnitude;
            float strideLengthen = 1f + flatVelocity * bobStrideSpeedLengthen;
            _headBobCycle += (flatVelocity / strideLengthen) * (Time.deltaTime / headBobFrequency);
            
            float bobFactor = Mathf.Sin(_headBobCycle * Mathf.PI * 2f);
            float bobSwayFactor = Mathf.Sin(_headBobCycle * Mathf.PI * 2f + Mathf.PI * 0.5f);
            bobFactor = 1f - (bobFactor * 0.5f + 1f);
            bobFactor *= bobFactor;
            
            if(flatVelocity < 0.1f)
            {
                _headBobFade = Mathf.Lerp(_headBobFade, 0f, Time.deltaTime);
            }
            else
            {
                _headBobFade = Mathf.Lerp(_headBobFade, 1f, Time.deltaTime);
            }
            
            float speedHeightFactor = 1 + (flatVelocity * bobHeightSpeedMultiplier);


            float xPos = -headBobSideMovement * bobSwayFactor;
            float yPos = _springPosition * jumpLandMove + bobFactor * headBocHeight * _headBobFade * speedHeightFactor;
            float xTilt = -_springPosition * jumpLandTilt;
            float zTilt = bobSwayFactor * headBobSwayAngle * _headBobFade;
            
            head.localPosition = _originalLocalPosition + new Vector3(xPos, yPos, 0f);
            head.localRotation = Quaternion.Euler(xTilt, 0f, zTilt);

        }
    }
}
