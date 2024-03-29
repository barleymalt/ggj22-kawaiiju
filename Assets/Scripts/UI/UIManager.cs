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
        [SerializeField] private ItemButton m_ItemOne;
        [SerializeField] private ItemButton m_ItemTwo;
        [SerializeField] private ItemButton m_ItemThree;

        [SerializeField] private RectTransform m_Menu;
        
        [Header("Debug UI")]
        [SerializeField] private Button m_IncreaseBarValue;
        [SerializeField] private Slider m_NeedsBar;
        [SerializeField] private float m_IncreaseAmount;

        private KawaiijuManager _kawaiijuManager;

        private void Awake()
        {
            // Game UI
            var itemsThrower = FindObjectOfType<ItemsThrower>();
            
            m_ItemOne.onClick.AddListener(itemsThrower.SpawnItemOne);
            m_ItemTwo.onClick.AddListener(itemsThrower.SpawnItemTwo);
            m_ItemThree.onClick.AddListener(itemsThrower.SpawnItemThree);
            
            itemsThrower.OnUpdateButtonOne_AddCallback(m_ItemOne.UpdateButtonIcon);
            itemsThrower.OnUpdateButtonTwo_AddCallback(m_ItemTwo.UpdateButtonIcon);
            itemsThrower.OnUpdateButtonThree_AddCallback(m_ItemThree.UpdateButtonIcon);
            
            m_Menu.gameObject.SetActive(true);
        }

        private void Start()
        {
            // DebugUI
            _kawaiijuManager = FindObjectOfType<KawaiijuManager>();

            m_IncreaseBarValue.onClick.AddListener(IncreaseNeedsBar);
            m_NeedsBar.maxValue = _kawaiijuManager.currentController.NeedsBarMaxValue;
            
            _kawaiijuManager.OnLevelUp_AddCallback(UpdateBarMaxValue);
        }

        private void Update()
        {
            m_NeedsBar.value = _kawaiijuManager.currentController.NeedsBarValue;
        }

        void UpdateBarMaxValue(int newMax)
        {
            m_NeedsBar.minValue = _kawaiijuManager.currentController.NeedsBarStartValue - _kawaiijuManager.currentController.StartSatisfaction;
            m_NeedsBar.maxValue = _kawaiijuManager.currentController.NeedsBarMaxValue;
        }

        void IncreaseNeedsBar()
        {
            _kawaiijuManager.currentController.IncreaseNeedsBar(m_IncreaseAmount);
        }
    }
}