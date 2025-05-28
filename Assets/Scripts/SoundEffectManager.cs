using System.Collections;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioClip sound, bool loop = false)
    {
        if (!loop)
        {
            StartCoroutine(PlaySoundRoutine(sound));
        }
        else
        {
            LoopSound(sound);
        }
    }

    private void LoopSound(AudioClip sound)
    {
        AudioSource spawnedSource = Instantiate(audioSource);
        spawnedSource.clip = sound;
        spawnedSource.loop = true;
        spawnedSource.Play();
    }

    public void StopSound(AudioClip soundToFind)
    {
        foreach (AudioSource foundSource in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (foundSource.clip == soundToFind)
            {
                Destroy(foundSource.gameObject);
            }
        }
    }

    private IEnumerator PlaySoundRoutine(AudioClip sound)
    {
        AudioSource spawnedAudioSource = Instantiate(audioSource);
        spawnedAudioSource.clip = sound;
        spawnedAudioSource.Play();

        yield return new WaitForSeconds(spawnedAudioSource.clip.length);

        Destroy(spawnedAudioSource.gameObject);
    }
}
