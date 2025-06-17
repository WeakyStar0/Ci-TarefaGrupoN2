using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [Header("Audio Clips")]
    public AudioClip clickSound;
    public AudioClip backgroundMusic;

    [Header("Volumes")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float effectsVolume = 1f;

    private AudioSource audioSource;  // efeitos
    private AudioSource musicSource;  // música de fundo

    private bool isMusicMuted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = effectsVolume;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = musicVolume;

        if (backgroundMusic != null)
            musicSource.Play();
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound, effectsVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (!isMusicMuted && musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsVolume = Mathf.Clamp01(volume);
        if (audioSource != null)
            audioSource.volume = effectsVolume;
    }

    // Função para ligar/desligar mute da música
    public void ToggleMusicMute()
    {
        if (musicSource == null) return;

        isMusicMuted = !isMusicMuted;

        if (isMusicMuted)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
            musicSource.volume = musicVolume;
        }
    }
}
