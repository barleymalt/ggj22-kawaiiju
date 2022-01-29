using System.Collections;
using System.Collections.Generic;
using Kawaiiju;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemButton : Button
{
    [SerializeField] private Image m_ButtonIcon;
    [SerializeField] private TextMeshProUGUI m_buttonText;

    private int _buttonDisableCountdown;
    
    
    public void UpdateButtonIcon(ItemData data)
    {
        m_ButtonIcon.sprite = data.Sprite;
        m_buttonText.text = data.Name;
        _buttonDisableCountdown = data.Satisfaction;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        StartCoroutine(C_TempDisableButton(_buttonDisableCountdown));
    }

    IEnumerator C_TempDisableButton(int countdown)
    {
        interactable = false;
        yield return new WaitForSecondsRealtime(countdown);
        interactable = true;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(ItemButton))]
public class MenuButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        // base.OnInspectorGUI();
    }
}

#endif