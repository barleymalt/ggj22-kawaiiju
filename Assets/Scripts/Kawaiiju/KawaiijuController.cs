using System;
using UnityEngine;
using UnityEngine.AI;

namespace Kawaiiju
{
    public class KawaiijuController : MonoBehaviour
    {
        public int StartSatisfaction = 8;

        [SerializeField] private float m_needsBarDropSpeed;

        [Space, ReadOnlyAttribute] public float NeedsBarValue;

        private KawaiijuManager _kawaiijuManager;

        public KawaiijuFetch Fetch { get; private set; }
        private NavMeshAgent NavAgent { get; set; }


        public float NeedsBarStartValue
        {
            get { return _kawaiijuManager.LevelMilestones[_kawaiijuManager.KawaiijuLevel - 1] + StartSatisfaction; }
        }

        public float NeedsBarMaxValue
        {
            get
            {
                try
                {
                    return _kawaiijuManager.LevelMilestones[_kawaiijuManager.KawaiijuLevel];
                }
                catch (Exception e)
                {
                    // Todo: opsies
                }

                return 0;
            }
        }

        public bool IsSatisfied
        {
            get { return NeedsBarValue - NeedsBarStartValue >= StartSatisfaction / 2; }
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
            Fetch.OnWantedNeedSatisfied_Item_AddCallback(OnWantedNeedSatisfied);
            Fetch.OnUnwantedNeedSatisfied_Item_AddCallback(OnUnwantedNeedSatisfied);
            Fetch.OnArbitraryNeedSatisfied_Item_AddCallback(OnArbitraryNeedSatisfied);
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