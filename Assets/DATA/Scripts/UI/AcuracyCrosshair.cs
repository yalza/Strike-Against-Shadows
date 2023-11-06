using DATA.Scripts.Core;
using DG.Tweening;
using UnityEngine;

namespace DATA.Scripts.UI
{
    public class AcuracyCrosshair : MonoBehaviour
    {
        #region Accuracy
        [Space]
        [Header("Acuracy")]
        public float acuracy = 5f;
        public RectTransform crossHairTop;
        public RectTransform crossHairLeft;
        public RectTransform crossHairRight;
        public RectTransform crossHairBottom;
        #endregion

        private void Start()
        {
            Observer.Instant.RegisterObserver(Constant.acuracyCrosshair, Acuracy);
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
    }
}