using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Akali.Ui_Materials.Scripts.Components
{
    public class HandSlideTween : MonoBehaviour
    {
        private const float Duration = 0.8f;

        private void OnEnable()
        {
            var image = gameObject.GetComponent<Image>();
            var value = image.rectTransform.anchoredPosition.x;
            image.rectTransform.DOAnchorPosX(-value, Duration)
                .OnComplete(() => image.rectTransform.DOAnchorPosX(value, Duration))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            gameObject.transform.DOKill();
        }
    }
}