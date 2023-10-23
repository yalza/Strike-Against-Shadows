using System;
using System.Collections;
using DATA.Scripts.Core;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DATA.Scripts.Weapon
{
    public enum WeaponType
    {
        Projectile,
        Raycast,
    }

    public enum Auto
    {
        Full,
        Semi,
    }

    public class Weapon : MonoBehaviour
    {
        #region Generals
        [Header("Generals")]
        public WeaponType type = WeaponType.Projectile;
        public Auto auto = Auto.Full;
        public GameObject weaponModel;
        private AudioSource _audioSource;

        #endregion 
        
        #region Fire

        [Space] [Header("Fire")] public float damage = 10f;
        public float delayBeforeFire = 0.1f;
        private bool _canFire = true;
        public Transform muzzleSpot;
        public GameObject muzzleFlashVfx;
        public GameObject explosionVfx;
        public AudioClip fireSfx;
        public float distanceRaycast = 1000f;
        private bool _isFiring;
        private GameObject _muzzleFlash;
        [Space]
        #endregion
        
        #region Accuracy
        [Space]
        [Header("Acuracy")]
        public float acuracy = 5f;
        public RectTransform crossHairTop;
        public RectTransform crossHairLeft;
        public RectTransform crossHairRight;
        public RectTransform crossHairBottom;
        
        [Space]
        #endregion
        

        #region Ammo
        [Space]
        [Header("Ammo")]
        public bool autoReload = true;
        public int clipSize = 30;
        public int curentAmmo = 30;
        public int maxAmmo = 120;
        public AudioClip reloadSfx;
        [Space]
        #endregion
        
        #region Recoil
        [Space]
        [Header("Recoil")]
        public float recoilKickBackMin = 0.1f;
        public float recoilKickBackMax = 0.3f;
        public float recoilRotationMin = 0.1f;
        public float recoilRotationMax = 0.3f;
        public float recoilRecoveryRate = 0.01f;
        private Quaternion _originRotation;
        private Vector3 _originPosition;
        #endregion

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            curentAmmo = clipSize;
            _originRotation = weaponModel.transform.localRotation;
            _originPosition = weaponModel.transform.localPosition;
        }
        
        private void OnEnable()
        {
            _audioSource.Stop();
            _canFire = true;
            _isFiring = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.R) && curentAmmo < clipSize && !_isFiring)
            {
                Reload();
            }

            if (CanFire())
            {
                if (auto == Auto.Full)
                {
                    if (Input.GetMouseButton(0))
                    {
                        _isFiring = true;
                        Fire();
                        Acuracy();
                        Recoil();
                        StartCoroutine(IEDelayFire(delayBeforeFire));
                    }
                    else
                    {
                        _isFiring = false;
                    }
                    
                }
                else if(auto == Auto.Semi)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Fire();
                        Acuracy();
                        Recoil();
                        StartCoroutine(IEDelayFire(delayBeforeFire));
                    }
                    
                }

            }


            Recover();

        }

        private void Acuracy()
        {
            
            crossHairTop.DOBlendableMoveBy(new Vector3(0, acuracy, 0), 0.1f);
            crossHairLeft.DOBlendableMoveBy(new Vector3(-acuracy, 0, 0), 0.1f);
            crossHairRight.DOBlendableMoveBy(new Vector3(acuracy, 0, 0), 0.1f);
            crossHairBottom.DOBlendableMoveBy(new Vector3(0, -acuracy, 0), 0.1f);
            
            // Recover
            crossHairTop.DOBlendableMoveBy(new Vector3(0, -acuracy, 0), 0.5f);
            crossHairLeft.DOBlendableMoveBy(new Vector3(+acuracy, 0, 0), 0.5f);
            crossHairRight.DOBlendableMoveBy(new Vector3(-acuracy, 0, 0), 0.5f);
            crossHairBottom.DOBlendableMoveBy(new Vector3(0, +acuracy, 0), 0.5f);
        }


        private void Fire()
        {
            // Muzzle Flash
            _muzzleFlash = ObjectPooling.Instant.GetGameObject(muzzleFlashVfx);
            _muzzleFlash.transform.SetParent(muzzleSpot);
            _muzzleFlash.transform.position = muzzleSpot.position;
            _muzzleFlash.transform.rotation = muzzleSpot.rotation;
            _muzzleFlash.SetActive(true);
            ObjectManager.Instant.StartDelayDeactive(05f,_muzzleFlash);
            
            _audioSource.clip = fireSfx;
            _audioSource.Play();
            
            curentAmmo--;
            if (type == WeaponType.Projectile)
            {
                
            }
            else if (type == WeaponType.Raycast)
            {
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                    if (Physics.Raycast(ray, out RaycastHit hit, distanceRaycast))
                    {
                        if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Enemy")
                        {
                            GameObject explosion = ObjectPooling.Instant.GetGameObject(explosionVfx);
                            explosion.transform.position = hit.point;
                            explosion.transform.rotation = Quaternion.LookRotation(hit.normal);
                            explosion.SetActive(true);
                            ObjectManager.Instant.StartDelayDeactive(0.1f,explosion);

                            Transform parent = hit.transform.parent;
                            IDamageable damageable = null;
                            while (parent != null)
                            {
                                damageable = parent.GetComponent<IDamageable>();
                                parent = parent.parent;
                            }
                            
                            
                            if(damageable!= null)
                                damageable.TakeDamage(damage);
                            
                        }
                        
                    }
                }
            }
        }

        private void Reload()
        {
            _canFire = false;
            muzzleFlashVfx.SetActive(false);
            _audioSource.clip = reloadSfx;
            _audioSource.Play();
            StartCoroutine(IEReloadAnimation());
            if (maxAmmo < clipSize) return;
            curentAmmo = clipSize;
            maxAmmo -= clipSize;
            
        }

        IEnumerator IEReloadAnimation()
        {
            _canFire = false;

            var localPosition = weaponModel.transform.localPosition;
            var localRotation = weaponModel.transform.localRotation;
            var position = localPosition +  new Vector3(0.35f, -0.3f, 1.2f);
            var rotation = Quaternion.Euler(localRotation.eulerAngles) * Quaternion.Euler(-28f,0f,-60f); 
            weaponModel.transform.DOBlendableMoveBy(position, 1f/6f);
            weaponModel.transform.DOBlendableRotateBy(rotation.eulerAngles, 1f/6f);
            
            yield return new WaitForSeconds(2.5f/6f);
            
            position = new Vector3(0, 0, -0.1f);
            weaponModel.transform.DOBlendableMoveBy(position, 1.5f/6f);
            
            yield return new WaitForSeconds(1.5f/6f);
            
            position = localPosition +  new Vector3(-0.25f, -0.4f, -0.6f);
            rotation = Quaternion.Euler(localRotation.eulerAngles) * Quaternion.Euler(28f,-30f,60f);
            weaponModel.transform.DOBlendableMoveBy(position, 4f/6f);
            weaponModel.transform.DOBlendableRotateBy(rotation.eulerAngles, 4f/6f);
            
            yield return new WaitForSeconds(5f/6f);
            
            position = localPosition + new Vector3(0.25f, 0.1f, 0.6f);
            rotation = Quaternion.Euler(localRotation.eulerAngles) * Quaternion.Euler(0,30,-180f);
            weaponModel.transform.DOBlendableMoveBy(position, 2.5f/6f);
            weaponModel.transform.DOBlendableRotateBy(rotation.eulerAngles, 2.5f/6f);

            yield return new WaitForSeconds(3f / 6f);
            _canFire = true;
        }

        private bool CanFire()
        {
            if (_canFire)
            {
                if (curentAmmo > 0)
                {
                    return true;
                }
                else
                {
                    if(autoReload)
                        Reload();
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private void Recoil()
        {
            // Calculate random values for the recoil position and rotation
            float kickBack = Random.Range(recoilKickBackMin, recoilKickBackMax);
            float kickRot = Random.Range(recoilRotationMin, recoilRotationMax);

            // Apply the random values to the weapon's postion and rotation
            weaponModel.transform.Translate(new Vector3(0, 0, -kickBack), Space.Self);
            weaponModel.transform.Rotate(new Vector3(-kickRot, 0, 0), Space.Self);
            
            
        }

        private void Recover()
        {
            weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, _originPosition, recoilRecoveryRate * Time.deltaTime);
            weaponModel.transform.localRotation = Quaternion.Lerp(weaponModel.transform.localRotation, _originRotation, recoilRecoveryRate * Time.deltaTime);
            
        }

        IEnumerator IEDelayFire(float time)
        {
            _canFire = false;
            yield return new WaitForSeconds(time);
            _canFire = true;
        }
        
    }
}
