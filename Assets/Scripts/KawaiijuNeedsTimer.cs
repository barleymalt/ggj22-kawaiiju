using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class KawaiijuNeedsTimer : MonoBehaviour
{
    // Set the timer to trigger between this two values
    [Header("Need Trigger Timer")]
    [SerializeField] private int m_TimerMin;
    [SerializeField] private int m_TimerMax;
    [Space]
    [SerializeField, ReadOnlyAttribute] private int timerCounter;
    [SerializeField, ReadOnlyAttribute] private int timerTarget;
    
    // How long the need stays after the timer triggerz
    [Header("Need Span")]
    [SerializeField] private float m_ShowNeedSpan;
    [Space]
    [SerializeField, ReadOnlyAttribute] private int needSpanCounter;
    
    
    private void Start()
    {
        StartCoroutine(C_TriggerNeedTimer());
    }

    private IEnumerator C_TriggerNeedTimer()
    {
        while (true)
        {
            // Timer for when the next need needs to be spawned
            timerCounter = 0;
            timerTarget = Random.Range(m_TimerMin, m_TimerMax);
            
            while (timerCounter < timerTarget)
            {
                yield return new WaitForSecondsRealtime(1);

                timerCounter += 1;
            }

            Debug.Log("Trigger need.");

            // Timer for how long the need will be visible
            needSpanCounter = 0;
            
            while (needSpanCounter < m_ShowNeedSpan)
            {
                yield return new WaitForSecondsRealtime(1);

                needSpanCounter += 1;
            }
            
            Debug.Log("Waited too long, need disappeared.");
        }
    }
}