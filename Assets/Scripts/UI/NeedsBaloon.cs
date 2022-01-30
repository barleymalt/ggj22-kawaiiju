using System.Collections;
using UnityEngine;

namespace Kawaiiju
{
    public class NeedsBaloon : MonoBehaviour
    {
        [SerializeField] private AudioClip m_FoodClip;
        [SerializeField] private AudioClip m_PlayClip;
        [SerializeField] private AudioClip m_RestClip;


        public void ShowNeedsBaloon(NEED_KIND kind)
        {
            gameObject.SetActive(true);
            
            StartCoroutine(C_ShowBaloon(kind));
        }

        public void HideNeedsBaloon()
        {
            gameObject.SetActive(false);

            SoundManager.Instance.StopSFX();
        }

        IEnumerator C_ShowBaloon(NEED_KIND kind)
        {
            switch (kind)
            {
                case NEED_KIND.Food:
                    SoundManager.Instance.PlaySFX(m_FoodClip);
                    break;
                case NEED_KIND.Play:
                    SoundManager.Instance.PlaySFX(m_PlayClip);
                    break;
                case NEED_KIND.Rest:
                    SoundManager.Instance.PlaySFX(m_RestClip);
                    break;
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }
}