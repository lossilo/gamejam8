using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }
}
