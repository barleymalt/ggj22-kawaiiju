using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Kawaiiju
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private NEED_KIND m_NeedKind;
        [SerializeField] private int m_SatisfactionAmount;

        private SpriteRenderer _spriteRenderer;
        private LookAtConstraint _lookAtConstraint;

        public NEED_KIND NeedKind
        {
            get { return m_NeedKind; }
        }

        public int SatisfactionAmount
        {
            get { return m_SatisfactionAmount; }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _lookAtConstraint = GetComponentInChildren<LookAtConstraint>();

            _lookAtConstraint.AddSource(
                new ConstraintSource
                {
                    sourceTransform = Camera.main.transform
                }
            );
        }

        public void InitializeItem(ItemData itemData)
        {
            m_NeedKind = itemData.Kind;
            m_SatisfactionAmount = itemData.Satisfaction;
            _spriteRenderer.sprite = itemData.Sprite;
        }
    }
}