using UnityEngine;

namespace Kawaiiju
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private NEED_KIND m_NeedKind;
        [SerializeField] private int m_SatisfactionAmount;
        
        public NEED_KIND NeedKind
        {
            get { return m_NeedKind; }
        }
        
        public int SatisfactionAmount
        {
            get { return m_SatisfactionAmount; }
        }
    }
}