using System;
using System.Collections.Generic;
using UnityEngine;
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


        private void Start()
        {
            UpdateSpawnableItems();
        }

        public void UpdateSpawnableItems()
        {
            m_ItemOneData = m_ItemsCollection.GetRandomItemData;
            m_ItemTwoData = m_ItemsCollection.GetRandomItemData;
            m_ItemThreeData = m_ItemsCollection.GetRandomItemData;
        }
        
        public void SpawnItemOne()
        {
            SpawnItem(m_ItemOneData);
        }

        public void SpawnItemTwo()
        {
            SpawnItem(m_ItemTwoData);
        }
        
        public void SpawnItemThree()
        {
            SpawnItem(m_ItemThreeData);
        }

        private void SpawnItem(ItemData itemData)
        {
            var spawnedItem = Instantiate(m_ItemPrefab);

            spawnedItem.InitializeItem(itemData);
            SpawnedItems.Add(spawnedItem);

            spawnedItem.transform.position = GetRandomSpawnPoint();
            
            UpdateSpawnableItems();
        }
        
        public void DestroyItem(Item itemToDestroy)
        {
            SpawnedItems.Remove(itemToDestroy);

            Destroy(itemToDestroy.gameObject);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
        }
    }
}