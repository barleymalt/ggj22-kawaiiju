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
            get { return Items[Random.Range(0, Items.Count)]; }
        }
    }

    [System.Serializable]
    public struct ItemData
    {
        public string Name;
        public int Satisfaction;

        [Space]
        public NEED_KIND Kind;
        public Sprite Sprite;
        public AudioClip Clip;
    }
}