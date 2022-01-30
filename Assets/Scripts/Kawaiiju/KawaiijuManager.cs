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

        public UnityEvent OnWin;
        public UnityEvent OnLose;

        private bool _gameOver;

        private UnityAction<int> _onLevelUp;
        public void OnLevelUp_AddCallback(UnityAction<int> a) => _onLevelUp += a;

        private UnityAction _onGameOver;
        public void OnGameOver_AddCallback(UnityAction a) => _onGameOver += a;


        private void Awake()
        {
            currentController = m_KawaiijuKid;
        }

        private void Start()
        {
            HandleLevelUp();
        }

        void Update()
        {
            if (_gameOver)
                return;

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
                _onLevelUp?.Invoke((int) currentController.NeedsBarMaxValue);

                HandleLevelUp();
            }
        }

        private bool OnDeath()
        {
            // If the needs bar value is lower than the current level minimum, death!
            if (currentController.NeedsBarValue < m_LevelMilestones[KawaiijuLevel - 1])
            {
                OnLose?.Invoke();
                SoundManager.Instance.PlayLoseMusic();

                Debug.Log("Kawaiiju ded!");

                _gameOver = true;
                _onGameOver?.Invoke();
                
                return true;
            }

            return false;
        }

        private bool OnMaxLevelReached()
        {
            // Check when the max level is reached, WIN CONDITION?
            if (KawaiijuLevel == m_LevelMilestones.Length)
            {
                OnWin?.Invoke();
                SoundManager.Instance.PlayWinMusic();

                Debug.Log("Max level reached!");

                _gameOver = true;
                _onGameOver?.Invoke();

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
                    SoundManager.Instance.ChangeBackgroundMusic(0);
                    break;
                case 2:
                    m_KawaiijuKid.gameObject.SetActive(false);
                    m_KawaiijuYoung.gameObject.SetActive(true);
                    currentController = m_KawaiijuYoung;
                    SoundManager.Instance.ChangeBackgroundMusic(1);
                    break;
                case 3:
                    m_KawaiijuYoung.gameObject.SetActive(false);
                    m_KawaiijuAdult.gameObject.SetActive(true);
                    currentController = m_KawaiijuAdult;
                    SoundManager.Instance.ChangeBackgroundMusic(2);
                    break;
            }
        }
    }
}