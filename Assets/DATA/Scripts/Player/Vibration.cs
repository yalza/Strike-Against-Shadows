
using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DATA.Scripts.Player
{
    public class Vibration : MonoBehaviour
    {
        public bool vibrateOnAwake;
        public float shakeSpeed = 60f;
        public float dereaseMultiplier = 0.9f;
        public int numberOfShakes = 10;


        private Vector3 _actualShakeDistance;
        private Quaternion _actualShakeRotation;
        private float _actualSpeed;
        private float _actualDereaseMultiplier;
        private float _actualNumberOfShakes;
        
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        private void Start()
        {
            var transform1 = transform;
            _originalPosition = transform1.localPosition;
            _originalRotation = transform1.localRotation;
        }

        public void StartShakingRandom(float minDistance, float maxDistance, float minRotation, float maxRotation)
        {
            _actualShakeDistance = new Vector3(Random.Range(minDistance,maxDistance),Random.Range(minDistance,maxDistance),Random.Range(minDistance,maxDistance));
            _actualShakeRotation = new Quaternion(Random.Range(minRotation,maxRotation),Random.Range(minRotation,maxRotation),Random.Range(minRotation,maxRotation),1);
            _actualSpeed = shakeSpeed * Random.Range(0.8f, 1.2f);
            _actualDereaseMultiplier = dereaseMultiplier * Random.Range(0.8f, 1.2f);
            _actualNumberOfShakes = numberOfShakes + Random.Range(-1, 1);
            StopShaking();
            StartCoroutine(Shake());
        }

        public void StartShaking(Vector3 shakeDistance, Quaternion shakeRotation,float speed, float diminish, int numberOfShake)
        {
            _actualShakeDistance = shakeDistance;
            _actualShakeRotation = shakeRotation;
            _actualSpeed = speed;
            _actualDereaseMultiplier = diminish;
            _actualNumberOfShakes = numberOfShake;
            StopShaking();
            StartCoroutine(Shake());
        }

        private void StopShaking()
        {
            StartCoroutine(Shake());
            var transform1 = transform;
            transform1.position = _originalPosition;
            transform1.rotation = _originalRotation;
        }

        private IEnumerator Shake()
        {
            var transform1 = transform;
            _originalPosition = transform1.localPosition;
            _originalRotation = transform1.localRotation;

            float hitTime = Time.time;
            float shake = _actualNumberOfShakes;

            float shakeDistanceX = _actualShakeDistance.x;
            float shakeDistanceY = _actualShakeDistance.y;
            float shakeDistanceZ = _actualShakeDistance.z;
            
            float shakeRotationX = _actualShakeRotation.x;
            float shakeRotationY = _actualShakeRotation.y;
            float shakeRotationZ = _actualShakeRotation.z;

            while (shake > 0)
            {
                float time = (Time.time - hitTime) * _actualSpeed;
                float x = _originalPosition.x + Mathf.Sin(time) * shakeDistanceX;
                float y = _originalPosition.y + Mathf.Sin(time) * shakeDistanceY;
                float z = _originalPosition.z + Mathf.Sin(time) * shakeDistanceZ;

                float xRot = _originalRotation.x + Mathf.Sin(time) * shakeRotationX;
                float yRot = _originalRotation.y + Mathf.Sin(time) * shakeRotationY;
                float zRot = _originalRotation.z + Mathf.Sin(time) * shakeRotationZ;
                transform1.position = new Vector3(x, y, z);
                transform.localRotation = new Quaternion(xRot, yRot, zRot, 1);


                hitTime = Time.time;
                shakeDistanceX *= _actualDereaseMultiplier;
                shakeDistanceY *= _actualDereaseMultiplier;
                shakeDistanceZ *= _actualDereaseMultiplier;
                shakeRotationX *= _actualDereaseMultiplier;
                shakeRotationY *= _actualDereaseMultiplier;
                shakeRotationZ *= _actualDereaseMultiplier;

                shake--;


                yield return true;
            }

            transform1.localPosition = _originalPosition;
            transform1.localRotation = _originalRotation;
        }
        
        
        
    }
}