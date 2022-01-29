using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kawaiiju
{

    public class ItemsThrower : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnPoints;
        
        [Space]
        [SerializeField] private Item m_ItemOne;
        [SerializeField] private Item m_ItemTwo;
        [SerializeField] private Item m_ItemThree;
        
        [ReadOnlyAttribute]
        public List<Item> SpawnedItems = new List<Item>();
        
        public void SpawnItemOne()
        {
            var spawnedItem = Instantiate(m_ItemOne);
            SpawnedItems.Add(spawnedItem);
            
            spawnedItem.transform.position = GetRandomSpawnPoint();
        }
        
        public void SpawnItemTwo()
        {
            var spawnedItem = Instantiate(m_ItemTwo);
            SpawnedItems.Add(spawnedItem);

            spawnedItem.transform.position = GetRandomSpawnPoint();
        }
        
        public void SpawnItemThree()
        {
            var spawnedItem = Instantiate(m_ItemThree);
            SpawnedItems.Add(spawnedItem);

            spawnedItem.transform.position = GetRandomSpawnPoint();
        }

        public void DestroyItem(Item itemToDestroy)
        {
            SpawnedItems.Remove(itemToDestroy);

            Destroy(itemToDestroy.gameObject);
        }

        Vector3 GetRandomSpawnPoint()
        {
            return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length - 1)].position;
        }
    }
}