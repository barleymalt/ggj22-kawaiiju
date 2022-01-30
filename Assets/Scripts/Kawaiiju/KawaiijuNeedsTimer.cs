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

        [Header("Sugar")]
        [SerializeField] private NeedsBaloon m_NeedsBaloon;

        private KawaiijuController _controller;
        private Coroutine _triggerNeedTimerCo;
        
        public NEED_KIND AppearedNeed
        {
            get { return m_AppearedNeed; }
        }

        private void Start()
        {
            m_NeedsBaloon.gameObject.SetActive(false);

            _controller = GetComponentInParent<KawaiijuController>();
            _controller.Fetch.OnWantedNeedSatisfied_AddCallback(m_NeedsBaloon.HideNeedsBaloon);
            _controller.Fetch.OnWantedNeedSatisfied_AddCallback(RestartTimer);
            
            _triggerNeedTimerCo = StartCoroutine(C_TriggerNeedTimer());
        }

        private IEnumerator C_TriggerNeedTimer()
        {
            while (true)
            {
                m_AppearedNeed = NEED_KIND.None;
                // Timer for when the next need needs to be spawned
                m_TimerCounter = 0;
                // Timer for how long the need will be visible
                m_NeedSpanCounter = 0;
                // How long before the nned appears
                m_TimerTarget = Random.Range(m_TimerRandomRange.x, m_TimerRandomRange.y);
                
                while (m_TimerCounter < m_TimerTarget)
                {
                    yield return new WaitForSecondsRealtime(1);

                    m_TimerCounter += 1;
                }

                m_AppearedNeed = GetRandomNeedKind();
                m_NeedsBaloon.ShowNeedsBaloon(m_AppearedNeed);

                Debug.Log("Need appeared.");
                

                while (m_NeedSpanCounter < m_ShowNeedSpan)
                {
                    yield return new WaitForSecondsRealtime(1);

                    m_NeedSpanCounter += 1;
                }

                m_NeedsBaloon.HideNeedsBaloon();

                Debug.Log("Waited too long, need disappeared.");
            }
        }

        private void RestartTimer()
        {
            StopCoroutine(_triggerNeedTimerCo);
            
            _triggerNeedTimerCo = StartCoroutine(C_TriggerNeedTimer());
        }

        static NEED_KIND GetRandomNeedKind()
        {
            return (NEED_KIND) Random.Range(1, System.Enum.GetValues(typeof(NEED_KIND)).Length);
        }
    }
}