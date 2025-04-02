using DG.Tweening;
using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class DOTweenExtensions
    {
        public static Tweener DOVolume(this AudioSource audioSource, float endValue, float duration)
        {
            return DOVirtual.Float(audioSource.volume, endValue, duration, value => audioSource.volume = value);
        }

        public static Sequence DOShakeLocalRotationZ(this Transform transform, float offset, float stepDuration, float steps, Sequence seq = null, object id = null)
        {
            seq ??= DOTween.Sequence();

            var origin = transform.localEulerAngles;
            var lower = origin - Vector3.forward * offset;
            var upper = origin + Vector3.forward * offset;
            
            seq.Append(transform.DOLocalRotate(lower, stepDuration * 0.25f).SetId(id));
            for (int i = 0; i < steps - 1; i++)
            {
                seq.Append(transform.DOLocalRotate(upper, stepDuration * 0.5f).SetId(id));
                seq.Append(transform.DOLocalRotate(lower, stepDuration * 0.5f).SetId(id));
            }
            seq.Append(transform.DOLocalRotate(origin, stepDuration * 0.25f).SetId(id));

            return seq;
        }

        public static Sequence DOShakeLocalPositionX(this Transform transform, float offset, float stepDuration, float steps, Sequence seq = null, object id = null)
        {
            seq ??= DOTween.Sequence();
            seq.SetLink(transform.gameObject);
            seq.SetId(id);

            var origin = transform.localPosition.x;
            var lower = origin - offset;
            var upper = origin + offset;
            
            seq.Append(transform.DOLocalMoveX(lower, stepDuration * 0.25f).SetId(id));
            for (int i = 0; i < steps - 1; i++)
            {
                seq.Append(transform.DOLocalMoveX(upper, stepDuration * 0.5f).SetId(id));
                seq.Append(transform.DOLocalMoveX(lower, stepDuration * 0.5f).SetId(id));
            }
            seq.Append(transform.DOLocalMoveX(origin, stepDuration * 0.25f).SetId(id));

            return seq;
        }
    }
}