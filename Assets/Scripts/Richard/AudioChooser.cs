using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChooser : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomAudioWithDelay());
    }

    IEnumerator PlayRandomAudioWithDelay()
    {
        while (true)
        {
            // Play a randomly selected audio clip
            PlayRandomAudioClip();

            // Wait for the audio clip to finish playing
            yield return new WaitForSeconds(audioSource.clip.length);

            // Wait for a random duration before playing the next audio clip
            float delay = Random.Range(7f, 15f);
            yield return new WaitForSeconds(delay);
        }
    }

    void PlayRandomAudioClip()
    {
        if (audioClips.Length > 0)
        {
            // Select a random audio clip
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            // Assign the selected clip to the AudioSource and play it
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No audio clips assigned to the array!");
        }
    }
}