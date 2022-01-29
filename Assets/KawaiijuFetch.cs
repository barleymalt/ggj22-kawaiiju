using System;
using System.Collections;
using System.Collections.Generic;
using Kawaiiju;
using UnityEngine;
using UnityEngine.Events;

namespace Kawaiiju
{
    public class KawaiijuFetch : MonoBehaviour
    {
        private KawaiijuNeedsTimer _kawaiijuNeedsTimer;

        // When the need is satisfied when needed
        private UnityAction<Item> _onWantedNeedSatisfied;
        public void OnWantedNeedSatisfied_AddCallback(UnityAction<Item> a) => _onWantedNeedSatisfied += a;
        
        // When the need is satisfied but wasn't the one the kawaiiju wanted
        private UnityAction<Item> _onUnwantedNeedSatisfied;
        public void OnUnwantedNeedSatisfied_AddCallback(UnityAction<Item> a) => _onUnwantedNeedSatisfied += a;

        // When a need is satisfied but the kawaiiju wasn't looking for anything
        private UnityAction _onArbitraryNeedSatisfied;
        public void OnArbitraryNeedSatisfied_AddCallback(UnityAction a) => _onArbitraryNeedSatisfied += a;


        private const string ITEM_TAG = "Item";

        private void Awake()
        {
            _kawaiijuNeedsTimer = FindObjectOfType<KawaiijuNeedsTimer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ITEM_TAG) || !other.TryGetComponent(out Item fetchedItem)) 
                return;
            
            // ARBITRARY: If the kawaiiju satisfies a need but it didn't want anything
            if (_kawaiijuNeedsTimer.AppearedNeed == NEED_KIND.None)
            {
                _onArbitraryNeedSatisfied?.Invoke();
                
                Debug.Log("Need satisfied when NOT needed!");
            }
            // WANTED: If the kawaiiju satisfies a need that it wants
            else if (fetchedItem.NeedKind == _kawaiijuNeedsTimer.AppearedNeed)
            {
                _onWantedNeedSatisfied?.Invoke(fetchedItem);
                
                Debug.Log("Wanted need satisfied when needed!");
            }
            // UNWANTED: If the kawaiiju satisfies a need that it doesn't want
            else
            {
                _onUnwantedNeedSatisfied?.Invoke(fetchedItem);
                
                Debug.Log("Need satisfied but not wanted");
            }
        }
    }
}