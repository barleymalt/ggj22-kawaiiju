using System.Collections.Generic;
using UnityEngine;

namespace Kawaiiju
{
    [CreateAssetMenu(fileName = "Items Collection", menuName = "Kawaiiju/New ItemsCollection", order = 0)]
    public class ItemsCollection : ScriptableObject
    {
        [SerializeField] private List<ItemData> Items = new List<ItemData>();

        public ItemData GetRandomItemData
        {
            get { return Items[Random.Range(0, Items.Count - 1)]; }
        }
    }

    [System.Serializable]
    public struct ItemData
    {
        public NEED_KIND Kind;
        public Sprite Sprite;
        public int Satisfaction;
    }
}