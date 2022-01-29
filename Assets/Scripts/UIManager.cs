using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawaiiju
{
    public class UIManager : MonoBehaviour
    {
        [Header("Game UI")]
        [SerializeField] private Button m_ItemOne;
        [SerializeField] private Button m_ItemTwo;
        [SerializeField] private Button m_ItemThree;

        [Header("Debug UI")]
        [SerializeField] private Button m_IncreaseBarValue;
        [SerializeField] private Slider m_NeedsBar;
        [SerializeField] private float m_IncreaseAmount;

        private KawaiijuNeedsBar _kawaiijuNeedsBar;

        private void Awake()
        {
            // Game UI
            var itemsThrower = FindObjectOfType<ItemsThrower>();
            
            m_ItemOne.onClick.AddListener(itemsThrower.SpawnItemOne);
            m_ItemTwo.onClick.AddListener(itemsThrower.SpawnItemTwo);
            m_ItemThree.onClick.AddListener(itemsThrower.SpawnItemThree);
            
            // DebugUI
            _kawaiijuNeedsBar = FindObjectOfType<KawaiijuNeedsBar>();

            m_IncreaseBarValue.onClick.AddListener(IncreaseNeedsBar);
            m_NeedsBar.maxValue = _kawaiijuNeedsBar.NeedsBarMaxValue;
        }

        private void Update()
        {
            m_NeedsBar.value = _kawaiijuNeedsBar.NeedsBarValue;
        }

        void IncreaseNeedsBar()
        {
            _kawaiijuNeedsBar.IncreaseNeedsBar(m_IncreaseAmount);
        }
    }
}