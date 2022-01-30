using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseOver : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    
    private void OnMouseEnter()
    {
        transform.DOScale(1.1f, .23f).SetEase(Ease.OutCubic);

        OnEnter?.Invoke();
    }
    
    private void OnMouseExit()
    {
        transform.DOScale(1f, .3f);

        OnExit?.Invoke();
    }
}
