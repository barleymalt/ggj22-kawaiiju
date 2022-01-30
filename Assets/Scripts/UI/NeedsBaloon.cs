using System.Collections;
using UnityEngine;

namespace Kawaiiju
{
    public class NeedsBaloon : MonoBehaviour
    {
        [SerializeField] private AudioClip m_FoodClip;
        [SerializeField] private AudioClip m_PlayClip;
        [SerializeField] private AudioClip m_RestClip;

        [SerializeField] private AudioSource _audioSource;


        public void ShowNeedsBaloon(NEED_KIND kind)
        {
            StartCoroutine(C_ShowBaloon(kind));
        }

        IEnumerator C_ShowBaloon(NEED_KIND kind)
        {
            switch (kind)
            {
                case NEED_KIND.Food:
                    _audioSource.PlayOneShot(m_FoodClip);
                    break;
                case NEED_KIND.Play:
                    _audioSource.PlayOneShot(m_PlayClip);
                    break;
                case NEED_KIND.Rest:
                    _audioSource.PlayOneShot(m_RestClip);
                    break;
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }
}