using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;

    private const string VOLUME_KEY = "MusicVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            LoadVolume();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        SaveVolume();
    }

    private void LoadVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat(VOLUME_KEY, 1.0f);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, audioSource.volume);
    }
}
