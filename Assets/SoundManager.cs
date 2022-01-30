using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip[] m_MusicTracks;
    [SerializeField] private AudioClip m_WinMusic;
    [SerializeField] private AudioClip m_LoseMusic;

    [SerializeField] private AudioSource m_MusicSource;
    [SerializeField] private AudioSource m_SFXSource;
    

    public void ChangeBackgroundMusic(int index)
    {
        m_MusicSource.clip = m_MusicTracks[index];
        m_MusicSource.Play();
    }

    public void PlayWinMusic()
    {
        m_MusicSource.clip = m_MusicTracks[0];
        m_MusicSource.Play();
        
        m_SFXSource.loop = false;
        m_SFXSource.clip = m_WinMusic;
        m_SFXSource.Play();
    }
    
    public void PlayLoseMusic()
    {
        m_MusicSource.clip = m_MusicTracks[0];
        m_MusicSource.Play();
        
        m_SFXSource.loop = false;
        m_SFXSource.clip = m_LoseMusic;
        m_SFXSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        m_SFXSource.loop = true;
        m_SFXSource.clip = sfxClip;
        m_SFXSource.Play();
    }

    public void StopSFX()
    {
        m_SFXSource.Stop();
    }
}