using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _sfxAudioSource, _musicAudioSource;

    //This is to avoid Comb filter
    private AudioClip _lastSFXAudioClip;
    private void ClearLastAudioclip() { _lastSFXAudioClip = null; }


    public void PlaySFX(AudioClip audioClip, float volume = 1f, float pitch = 1f)
    {
        if (_lastSFXAudioClip == audioClip) return;
        CancelInvoke();

        _sfxAudioSource.volume = volume;
        _sfxAudioSource.pitch = pitch;

        _sfxAudioSource.PlayOneShot(audioClip);

        _lastSFXAudioClip = audioClip;

        Invoke(nameof(ClearLastAudioclip), 0.05f);
    }

    public void PlayRandomSFX(AudioClip[] audioClipList, float volume = 1f)
    {
        AudioClip randomClip = audioClipList[Random.Range(0, audioClipList.Length)];

        PlaySFX(randomClip, volume);
    }
        
    public void PlayMusic(AudioClip audioClip, float volume = 1f)
    {
        if (_musicAudioSource.isPlaying && _musicAudioSource.clip == audioClip) return;
        _musicAudioSource.volume = volume;

        _musicAudioSource.clip = audioClip;
        _musicAudioSource.Play();
    }
}
