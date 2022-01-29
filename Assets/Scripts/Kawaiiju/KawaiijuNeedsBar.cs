using System;
using UnityEngine;
using UnityEngine.AI;

namespace Kawaiiju
{
    public class KawaiijuNeedsBar : MonoBehaviour
    {
        // [SerializeField] private float m_needsBarStartValue;
        [SerializeField] private float m_needsBarDropSpeed;

        [Space, ReadOnlyAttribute] public float NeedsBarValue;

        private KawaiijuManager _kawaiijuManager;
        private KawaiijuFetch _fetch;
        
        private NavMeshAgent _navAgent;

        public float NeedsBarStartValue
        {
            get { return _kawaiijuManager.LevelMilestones[_kawaiijuManager.KawaiijuLevel - 1] + 10; }
        }

        public float NeedsBarMaxValue
        {
            get { return _kawaiijuManager.LevelMilestones[_kawaiijuManager.LevelMilestones.Length - 1]; }
        }

        private void OnEnable()
        {
            _kawaiijuManager = GetComponentInParent<KawaiijuManager>();
            
            _navAgent = GetComponentInParent<NavMeshAgent>();
            _fetch = GetComponentInChildren<KawaiijuFetch>();
        }

        private void Start()
        {
            NeedsBarValue = NeedsBarStartValue;
            
            InitNeedEvents();
        }

        private void Update()
        {
            // Decrease bar constantly
            NeedsBarValue -= m_needsBarDropSpeed * Time.deltaTime;
            
            // Todo: don't move if dead
            if (_fetch.ClosestItem && _navAgent.velocity.magnitude < .1f)
            {
                _navAgent.SetDestination(_fetch.ClosestItem.position);
            }
        }
        
        private void InitNeedEvents()
        {
            var kawaiijuFetch = GetComponentInChildren<KawaiijuFetch>();

            kawaiijuFetch.OnWantedNeedSatisfied_AddCallback(OnWantedNeedSatisfied);
            kawaiijuFetch.OnUnwantedNeedSatisfied_AddCallback(OnUnwantedNeedSatisfied);
            kawaiijuFetch.OnArbitraryNeedSatisfied_AddCallback(OnArbitraryNeedSatisfied);
        }

        private void OnWantedNeedSatisfied(Item fetchedItem)
        {
            IncreaseNeedsBar(fetchedItem.SatisfactionAmount);
        }

        private void OnUnwantedNeedSatisfied(Item fetchedItem)
        {
            DecreaseNeedsBar((float) fetchedItem.SatisfactionAmount / 2);
        }

        private void OnArbitraryNeedSatisfied()
        {
            IncreaseNeedsBar(1);
        }

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
