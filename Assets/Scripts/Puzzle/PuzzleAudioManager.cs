using UnityEngine;

public class PuzzleAudioManager : MonoBehaviour
{
    public static PuzzleAudioManager Instance;

    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

public void PlayCorrectSound(int correctCount)
{
    if (correctSound != null)
    {
        float basePitch = 0.8f;
        float pitchIncrement = 0.05f; // cada acerto aumenta o pitch

        float pitch = basePitch + (correctCount * pitchIncrement);

        // Criar AudioSource temporário para evitar sobrescrever outros sons
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.SetParent(transform);
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();
        tempSource.clip = correctSound;
        tempSource.pitch = pitch;
        tempSource.Play();

        Destroy(tempGO, correctSound.length / pitch); // destruir após tocar
    }
}



    public void PlayWrongSound()
    {
        if (wrongSound != null)
        {
            audioSource.pitch = 1f; // garantir pitch normal
            audioSource.PlayOneShot(wrongSound);
        }
    }
}
