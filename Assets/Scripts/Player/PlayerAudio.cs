using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private Dictionary<string, AudioClip> playerAudioClips = new Dictionary<string, AudioClip>();
    private bool canPlayAudio = true;
    private bool audioAloud = false;
    private AudioSource audioSource;
    private AudioSource stoppableAudioSource;
    private void Awake()
    {
       AudioSource[] audioSources = transform.GetChild(1).GetComponents<AudioSource>();
        audioSource = audioSources[0];
        stoppableAudioSource = audioSources[1];
        AudioClip[] audioclips = Resources.LoadAll<AudioClip>("Audio/SoundEffects/Player");
        foreach (AudioClip audioclip in audioclips) {
            playerAudioClips.Add(audioclip.name, audioclip);
        }
    }
    public void PlaySound(string audioSourceName) {
        audioSource.PlayOneShot(playerAudioClips[audioSourceName]);

    }
    public void PlaySoundNoOverlap(string audioSourceName)
    {
        if (canPlayAudio && audioAloud)
        {
            stoppableAudioSource.PlayOneShot(playerAudioClips[audioSourceName]);
            canPlayAudio = false;
            Invoke("StartSound", playerAudioClips[audioSourceName].length);
        }
    }
    
    public void ToggleOffStoppableAudio() {
        stoppableAudioSource.Stop();
        audioAloud = false;

    }
    public void StopAudio() {
        stoppableAudioSource.Stop();
    }
    public void StartStoppableAudio() {
        audioAloud = true;
    }
    private void StartSound() {
        canPlayAudio = true;
    }
}
