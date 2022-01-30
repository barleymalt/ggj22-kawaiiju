using UnityEngine;
using UnityEngine.Events;

public class OnMouseOver : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    
    private void OnMouseEnter()
    {
        OnEnter?.Invoke();
    }
    
    private void OnMouseExit()
    {
        OnExit?.Invoke();
    }
}
