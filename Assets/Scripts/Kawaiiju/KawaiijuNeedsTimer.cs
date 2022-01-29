using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Kawaiiju
{
    public enum NEED_KIND
    {
        None,
        Food,
        Play,
        Rest
    }

    public class KawaiijuNeedsTimer : MonoBehaviour
    {
        // Set the timer to trigger between this two values
        [Header("Need Trigger Timer")]
        [SerializeField] private Vector2Int m_TimerRandomRange;
        [Space]
        [SerializeField, ReadOnlyAttribute] private int m_TimerCounter;
        [SerializeField, ReadOnlyAttribute] private int m_TimerTarget;

        // How long the need stays after the timer triggerz
        [Header("Need Span")]
        [SerializeField] private float m_ShowNeedSpan;
        [SerializeField, ReadOnlyAttribute] private int m_NeedSpanCounter;

        [Space]
        [SerializeField, ReadOnlyAttribute] private NEED_KIND m_AppearedNeed;

        [Space]
        [SerializeField] private SpriteRenderer m_NeedsBaloon;
        
        public NEED_KIND AppearedNeed
        {
            get { return m_AppearedNeed; }
        }

        private void Start()
        {
            StartCoroutine(C_TriggerNeedTimer());
        }

        private IEnumerator C_TriggerNeedTimer()
        {
            while (true)
            {
                // Timer for when the next need needs to be spawned
                m_TimerCounter = 0;
                m_TimerTarget = Random.Range(m_TimerRandomRange.x, m_TimerRandomRange.y);

                m_NeedsBaloon.gameObject.SetActive(false);   // Todo: dotween

                while (m_TimerCounter < m_TimerTarget)
                {
                    yield return new WaitForSecondsRealtime(1);

                    m_TimerCounter += 1;
                }

                m_AppearedNeed = GetRandomNeedKind();
                m_NeedsBaloon.gameObject.SetActive(true);   // Todo: dotween
                
                Debug.Log("Need appeared.");

                // Timer for how long the need will be visible
                m_NeedSpanCounter = 0;

                while (m_NeedSpanCounter < m_ShowNeedSpan)
                {
                    yield return new WaitForSecondsRealtime(1);

                    m_NeedSpanCounter += 1;
                }

                m_AppearedNeed = NEED_KIND.None;
                m_NeedsBaloon.gameObject.SetActive(false);   // Todo: dotween

                Debug.Log("Waited too long, need disappeared.");
            }
        }

        static NEED_KIND GetRandomNeedKind()
        {
            return (NEED_KIND) Random.Range(1, System.Enum.GetValues(typeof(NEED_KIND)).Length);
        }
    }
}