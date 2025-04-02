using System;
using UnityEngine;

namespace _1Core.Scripts.Game
{
    [Serializable]
    public class ResetModel
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector3 _startScale;

        public virtual void Init(Transform tr)
        {
            _startPosition = tr.position;
            _startRotation = tr.rotation;
            _startScale = tr.localScale;
        }

        public virtual void Reset(Transform tr)
        {
            tr.position = _startPosition;
            tr.transform.rotation = _startRotation;
            tr.transform.localScale = _startScale;
            tr.gameObject.SetActive(true);
        }
    }
}