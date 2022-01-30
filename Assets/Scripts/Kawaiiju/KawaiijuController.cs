using System;
using UnityEngine;
using UnityEngine.AI;

namespace Kawaiiju
{
    public class KawaiijuController : MonoBehaviour
    {
        [SerializeField] private float m_needsBarDropSpeed;

        [Space, ReadOnlyAttribute] public float NeedsBarValue;

        private KawaiijuManager _kawaiijuManager;
        
        public KawaiijuFetch Fetch { get; private set; }
        private NavMeshAgent NavAgent { get; set; }

        private const int SATISFACTION_DELTA = 10;
        
        public float NeedsBarStartValue
        {
            get { return _kawaiijuManager.LevelMilestones[_kawaiijuManager.KawaiijuLevel - 1] + SATISFACTION_DELTA; }
        }

        public float NeedsBarMaxValue
        {
            get { return _kawaiijuManager.LevelMilestones[_kawaiijuManager.LevelMilestones.Length - 1]; }
        }

        public bool IsSatisfied
        {
            get { return NeedsBarValue - NeedsBarStartValue >= SATISFACTION_DELTA / 2; }
        }

        private void OnEnable()
        {
            _kawaiijuManager = GetComponentInParent<KawaiijuManager>();
            
            NavAgent = GetComponentInParent<NavMeshAgent>();
            Fetch = GetComponentInChildren<KawaiijuFetch>();
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
            if (Fetch.ClosestItem && NavAgent.velocity.magnitude < .1f)
            {
                NavAgent.SetDestination(Fetch.ClosestItem.position);
            }
        }
        
        private void InitNeedEvents()
        {
            Fetch.OnWantedNeedSatisfied_AddCallback(OnWantedNeedSatisfied);
            Fetch.OnUnwantedNeedSatisfied_AddCallback(OnUnwantedNeedSatisfied);
            Fetch.OnArbitraryNeedSatisfied_AddCallback(OnArbitraryNeedSatisfied);
        }

        private void OnWantedNeedSatisfied(Item fetchedItem)
        {
            IncreaseNeedsBar(fetchedItem.SatisfactionAmount);
        }

        private void OnUnwantedNeedSatisfied(Item fetchedItem)
        {
            DecreaseNeedsBar(fetchedItem.SatisfactionAmount);
        }

        private void OnArbitraryNeedSatisfied(Item fetchedItem)
        {
            DecreaseNeedsBar((float) fetchedItem.SatisfactionAmount / 2);
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
