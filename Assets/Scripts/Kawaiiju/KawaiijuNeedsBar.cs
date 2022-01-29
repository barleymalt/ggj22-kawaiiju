using UnityEngine;

namespace Kawaiiju
{
    public class KawaiijuNeedsBar : MonoBehaviour
    {
        [SerializeField] private float m_needsBarStartValue;
        [SerializeField] private float m_needsBarDropSpeed;

        [Space]
        [SerializeField] private int[] m_LevelMilestones;

        [Space, ReadOnlyAttribute] public float NeedsBarValue;
        [ReadOnlyAttribute] public int KawaiijuLevel = 1;

        public float NeedsBarStartValue
        {
            get { return m_needsBarStartValue; }
        }

        public float NeedsBarMaxValue
        {
            get { return m_LevelMilestones[m_LevelMilestones.Length - 1]; }
        }

        private void Start()
        {
            NeedsBarValue = m_needsBarStartValue;
        }

        private void Update()
        {
            if (OnMaxLevelReached())
                return;

            if (OnDeath())
                return;

            OnLevelUp();

            // Decrease bar constantly
            NeedsBarValue -= m_needsBarDropSpeed * Time.deltaTime;
        }

        #region CORE

        private void OnLevelUp()
        {
            // Level up when a milestone is reached
            if (NeedsBarValue > m_LevelMilestones[KawaiijuLevel])
            {
                Debug.Log("Level up!");

                KawaiijuLevel++;
            }
        }

        private bool OnDeath()
        {
            // If the needs bar value is lower than the current level minimum, death!
            if (NeedsBarValue < m_LevelMilestones[KawaiijuLevel - 1])
            {
                Debug.Log("Kawaiiju ded!");

                return true;
            }

            return false;
        }

        private bool OnMaxLevelReached()
        {
            // Check when the max level is reached, WIN CONDITION?
            if (KawaiijuLevel == m_LevelMilestones.Length)
            {
                Debug.Log("Max level reached!");

                return true;
            }

            return false;
        }

        #endregion

        public void IncreaseNeedsBar(float amount)
        {
            NeedsBarValue += amount;

            Debug.Log("Bar increased of " + amount);
        }

        public void DecreaseNeedsBar(float amount)
        {
            NeedsBarValue -= amount;
            
            Debug.Log("Bar decreased of " + amount);
        }
    }
}
