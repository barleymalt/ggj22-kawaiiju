using System;
using UnityEngine;

namespace Kawaiiju
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private NEED_KIND m_NeedKind;
        [SerializeField] private int m_SatisfactionAmount;

        private SpriteRenderer _spriteRenderer;
        
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
        }

        public void InitializeItem(ItemData itemData)
        {
            m_NeedKind = itemData.Kind;
            m_SatisfactionAmount = itemData.Satisfaction;
            _spriteRenderer.sprite = itemData.Sprite;
        }
    }
}