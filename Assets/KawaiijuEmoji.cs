using System;
using System.Collections;
using System.Collections.Generic;
using Kawaiiju;
using UnityEngine;

public class KawaiijuEmoji : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_HeadSprite;
    
    [Space]
    [SerializeField] private Sprite m_NormalFace;
    [SerializeField] private Sprite m_SatisfiedFace;
    [SerializeField] private Sprite m_AngryFace;
    [SerializeField] private Sprite m_HappyFace;

    private KawaiijuController _controller;

    
    private void Start()
    {
        _controller = GetComponentInParent<KawaiijuController>();
        
        _controller.Fetch.OnWantedNeedSatisfied_AddCallback(OnWantedNeedSatisfied);
        _controller.Fetch.OnArbitraryNeedSatisfied_AddCallback(OnArbitraryNeedSatisfied);
        _controller.Fetch.OnUnwantedNeedSatisfied_AddCallback(OnUnwantedNeedSatisfied);
    }

    private void OnWantedNeedSatisfied(Item i)
    {
        StartCoroutine(C_SwapFace(m_HappyFace));
    }
    
    private void OnUnwantedNeedSatisfied(Item i)
    {
        StartCoroutine(C_SwapFace(m_AngryFace));
    }
    
    private void OnArbitraryNeedSatisfied()
    {
        StartCoroutine(C_SwapFace(m_AngryFace));
    }

    IEnumerator C_SwapFace(Sprite swapSprite)
    {
        m_HeadSprite.sprite = swapSprite;
        
        yield return new WaitForSecondsRealtime(1);

        m_HeadSprite.sprite = _controller.IsSatisfied ? m_SatisfiedFace : m_NormalFace;
    }
}
