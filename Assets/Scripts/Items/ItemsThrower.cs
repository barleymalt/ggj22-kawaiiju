using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Kawaiiju
{
    public class ItemsThrower : MonoBehaviour
    {
        [SerializeField] private ItemsCollection[] m_ItemsCollections;

        [Space]
        [SerializeField] private Item m_ItemPrefab;
        [SerializeField] private int m_ButtonsUpdateTimer;
        
        [Space, SerializeField] private Transform[] m_SpawnPoints;

        public List<Item> SpawnedItems { get; } = new List<Item>();

        private ItemData m_ItemOneData;
        private ItemData m_ItemTwoData;
        private ItemData m_ItemThreeData;

        private KawaiijuManager _kawaiijuManager;

        private Coroutine _updateItemsCycleCo;

        private UnityAction<ItemData> _onUpdateButtonOne;
        public void OnUpdateButtonOne_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonOne += a;
        private UnityAction<ItemData> _onUpdateButtonTwo;
        public void OnUpdateButtonTwo_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonTwo += a;
        private UnityAction<ItemData> _onUpdateButtonThree;
        public void OnUpdateButtonThree_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonThree += a;


        private void Awake()
        {
            _kawaiijuManager = FindObjectOfType<KawaiijuManager>();
            
            _kawaiijuManager.OnGameOver_AddCallback(StopUpdateItemsCyle);
        }

        private void Start()
        {
            UpdateSpawnableItems(1);
            UpdateSpawnableItems(2);
            UpdateSpawnableItems(3);

            _updateItemsCycleCo = StartCoroutine(C_UpdateItemsCycle());
        }

        public void SpawnItemOne()
        {
            SpawnItem(m_ItemOneData);

            UpdateSpawnableItems(1);
        }

        public void SpawnItemTwo()
        {
            SpawnItem(m_ItemTwoData);

            UpdateSpawnableItems(2);
        }

        public void SpawnItemThree()
        {
            SpawnItem(m_ItemThreeData);

            UpdateSpawnableItems(3);
        }

        private void SpawnItem(ItemData itemData)
        {
            var spawnedItem = Instantiate(m_ItemPrefab);

            spawnedItem.InitializeItem(itemData);
            SpawnedItems.Add(spawnedItem);

            spawnedItem.transform.position = GetRandomSpawnPoint();
        }

        public void DestroyItem(Item itemToDestroy)
        {
            SpawnedItems.Remove(itemToDestroy);

            Destroy(itemToDestroy.gameObject);
        }

        private void UpdateSpawnableItems(int buttonIndex)
        {
            var collection = m_ItemsCollections[_kawaiijuManager.KawaiijuLevel - 1];

            switch (buttonIndex)
            {
                case 1:
                    m_ItemOneData = collection.GetRandomItemData;
                    _onUpdateButtonOne?.Invoke(m_ItemOneData);
                    break;
                case 2:
                    m_ItemTwoData = collection.GetRandomItemData;
                    _onUpdateButtonTwo?.Invoke(m_ItemTwoData);
                    break;
                case 3:
                    m_ItemThreeData = collection.GetRandomItemData;
                    _onUpdateButtonThree?.Invoke(m_ItemThreeData);
                    break;
            }
        }

        IEnumerator C_UpdateItemsCycle()
        {
            while (true)
            {
                for (int i = 1; i <= 3; i++)
                {
                    yield return new WaitForSeconds(m_ButtonsUpdateTimer);

                    UpdateSpawnableItems(i);
                }
            }
        }

        void StopUpdateItemsCyle()
        {
            StopCoroutine(C_UpdateItemsCycle());
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
        }
    }
}