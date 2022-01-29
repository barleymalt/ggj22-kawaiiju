using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button m_IncreaseBarValue;
    [SerializeField] private Slider m_NeedsBar;
    [SerializeField] private float m_IncreaseAmount;
    
    private KawaiijuNeedsBar _kawaiijuNeedsBar;
    
    private void Awake()
    {
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
