using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Kawaiiju
{

    public class ItemsThrower : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnPoints;
        [SerializeField] private ItemsCollection m_ItemsCollection;
        
        [Space]
        [SerializeField] private Item m_ItemPrefab;
        [Space]
        [SerializeField] private ItemData m_ItemOneData;
        [SerializeField] private ItemData m_ItemTwoData;
        [SerializeField] private ItemData m_ItemThreeData;
        
        [ReadOnlyAttribute]
        public List<Item> SpawnedItems = new List<Item>();

        
        private UnityAction<ItemData> _onUpdateButtonOne;
        public void OnUpdateButtonOne_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonOne += a;
        private UnityAction<ItemData> _onUpdateButtonTwo;
        public void OnUpdateButtonTwo_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonTwo += a;
        private UnityAction<ItemData> _onUpdateButtonThree;
        public void OnUpdateButtonThree_AddCallback(UnityAction<ItemData> a) => _onUpdateButtonThree += a;


        private void Start()
        {
            UpdateSpawnableItems(1);
            UpdateSpawnableItems(2);
            UpdateSpawnableItems(3);
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
            switch (buttonIndex)
            {
                case 1:
                    m_ItemOneData = m_ItemsCollection.GetRandomItemData;
                    _onUpdateButtonOne?.Invoke(m_ItemOneData);
                    break;
                case 2:
                    m_ItemTwoData = m_ItemsCollection.GetRandomItemData;
                    _onUpdateButtonTwo?.Invoke(m_ItemTwoData);
                    break;
                case 3:
                    m_ItemThreeData = m_ItemsCollection.GetRandomItemData;
                    _onUpdateButtonThree?.Invoke(m_ItemThreeData);
                    break;
            }
        }
        
        private Vector3 GetRandomSpawnPoint()
        {
            return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
        }
    }
}