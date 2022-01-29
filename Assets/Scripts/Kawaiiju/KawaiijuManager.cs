using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Kawaiiju
{
    public class KawaiijuManager : MonoBehaviour
    {
        [SerializeField] private Transform m_Cursor;

        private NavMeshAgent _navAgent;

        private KawaiijuNeedsBar _needsBar;
        private KawaiijuFetch _fetch;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _needsBar = GetComponentInChildren<KawaiijuNeedsBar>();
            _fetch = GetComponentInChildren<KawaiijuFetch>();
        }

        private void Start()
        {
            InitNeedEvents();
        }

        void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_fetch.ClosestItem && _navAgent.velocity.magnitude < .1f)
            {
                _navAgent.SetDestination(_fetch.ClosestItem.position);
            }
        }

        private void InitNeedEvents()
        {
            var kawaiijuFetch = FindObjectOfType<KawaiijuFetch>();

            kawaiijuFetch.OnWantedNeedSatisfied_AddCallback(OnWantedNeedSatisfied);
            kawaiijuFetch.OnUnwantedNeedSatisfied_AddCallback(OnUnwantedNeedSatisfied);
            kawaiijuFetch.OnArbitraryNeedSatisfied_AddCallback(OnArbitraryNeedSatisfied);
        }

        private void OnWantedNeedSatisfied(Item fetchedItem)
        {
            _needsBar.IncreaseNeedsBar(fetchedItem.SatisfactionAmount);
        }

        private void OnUnwantedNeedSatisfied(Item fetchedItem)
        {
            _needsBar.DecreaseNeedsBar((float) fetchedItem.SatisfactionAmount / 2);
        }

        private void OnArbitraryNeedSatisfied()
        {
            _needsBar.IncreaseNeedsBar(1);
        }
    }
}