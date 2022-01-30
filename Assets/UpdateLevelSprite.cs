using System;
using System.Collections;
using System.Collections.Generic;
using Kawaiiju;
using UnityEngine;

public class UpdateLevelSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] m_SpritesToSwap;


    // Start is called before the first frame update
    void Start()
    {
        var kawaiijuManager = FindObjectOfType<KawaiijuManager>();

        kawaiijuManager.OnLevelUp_AddCallback(UpdateSprite);
    }

    void UpdateSprite(int index)
    {
        foreach (var spriteRenderer in m_SpritesToSwap)
        {
            spriteRenderer.gameObject.SetActive(false);
        }

        Debug.Log(index);

        try
        {
            m_SpritesToSwap[index - 1].gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}