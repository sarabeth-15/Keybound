using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound,0.5f);
    }
}
