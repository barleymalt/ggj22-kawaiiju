using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Kawaiiju
{
    public class KawaiijuManager : MonoBehaviour
    {
        [SerializeField] private KawaiijuController m_KawaiijuKid;
        [SerializeField] private KawaiijuController m_KawaiijuYoung;
        [SerializeField] private KawaiijuController m_KawaiijuAdult;
        
        [Space, SerializeField] private int[] m_LevelMilestones;
        [Space, ReadOnlyAttribute] public int KawaiijuLevel = 1;

        [ReadOnlyAttribute]
        public KawaiijuController currentController;

        public int[] LevelMilestones
        {
            get { return m_LevelMilestones; }
        }

       public UnityEvent OnMaxLevel;
       // public UnityEvent OnDeath;

        private void Start()
        {
            HandleLevelUp();
        }

        void Update()
        {
            if (OnMaxLevelReached())
                return;

            if (OnDeath())
                return;

            OnLevelUp();
        }

        #region CORE

        private void OnLevelUp()
        {
            // Level up when a milestone is reached
            if (currentController.NeedsBarValue > m_LevelMilestones[KawaiijuLevel])
            {
                Debug.Log("Level up!");

                KawaiijuLevel++;
                
                HandleLevelUp();
            }
        }

        private bool OnDeath()
        {
            // If the needs bar value is lower than the current level minimum, death!
            if (currentController.NeedsBarValue < m_LevelMilestones[KawaiijuLevel - 1])
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
                OnMaxLevel?.Invoke();
                
                Debug.Log("Max level reached!");

                return true;
            }

            return false;
        }

        #endregion

        private void HandleLevelUp()
        {
            switch (KawaiijuLevel)
            {
                case 1:
                    currentController = m_KawaiijuKid;
                    m_KawaiijuKid.gameObject.SetActive(true);
                    break;
                case 2:
                    m_KawaiijuKid.gameObject.SetActive(false);
                    m_KawaiijuYoung.gameObject.SetActive(true);
                    currentController = m_KawaiijuYoung;
                    break;
                case 3:
                    m_KawaiijuYoung.gameObject.SetActive(false);
                    m_KawaiijuAdult.gameObject.SetActive(true);
                    currentController = m_KawaiijuAdult;
                    break;
            }
        }
    }
}