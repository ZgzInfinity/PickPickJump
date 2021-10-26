using UnityEngine;
using System.Collections.Generic;

/**
 * Script that controls the reproduction of the sounds
 */

public class AudioManager : MonoBehaviour
{
    // Static instance of the class
    public static AudioManager Instance;

    // Reference to the music reproductor
    public AudioSource musicAudioSource;

    // Reference to the effects reproductor
    public AudioSource effectsAudioSource;

    public List<Sound> soundtracks = new List<Sound>();

    public int currentSoundtrack;

    private bool soundtrackChanged;

    private float decreaseFactor = 0.5f;

    private float secondsToChangeSoundtrack = 0f;

    private float secondsPerSoundtrackChange = 1f;

    // Constructor
    private void Awake()
    {
        Instance = this;
        currentSoundtrack = 0;
        soundtrackChanged = false;
    }

    public void ChangeCurrentSoundtrack()
    {
        if (!soundtrackChanged)
        {
            soundtrackChanged = true;
            secondsToChangeSoundtrack = secondsPerSoundtrackChange;
            currentSoundtrack = (currentSoundtrack < soundtracks.Count - 1) ? currentSoundtrack + 1 : 0;
            Debug.Log(currentSoundtrack);
            PlaySound(soundtracks[currentSoundtrack]);
        }
    }

    // Reproduce a sound of the game
    public void StopSound(Sound sound)
    {
        // Check if the sound is a music or an effect
        if (sound.soundType == Sound.SoundType.MUSIC)
        {
            // Music
            StopMusic(sound);
        }
        else if (sound.soundType == Sound.SoundType.FX)
        {
            // Effect
            StopFx(sound);
        }
    }


    private void Update()
    {
        if (soundtrackChanged)
        {
            AllowNextChangeSoundtrack();
        }
    }

    private void AllowNextChangeSoundtrack()
    {
        if (secondsToChangeSoundtrack > 0f)
        {
            secondsToChangeSoundtrack -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            soundtrackChanged = false;
        }
    }

    // Play music
    private void playMusic(Sound sound)
    {
        // Set the clip with the volume and the loop options
        musicAudioSource.clip = sound.clip;
        musicAudioSource.volume = sound.volume;
        musicAudioSource.loop = sound.loop;

        // Play the music
        getMusicStatus();
        musicAudioSource.Play();
    }

    // Play music
    private void StopMusic(Sound sound)
    {
        musicAudioSource.Stop();
    }

    private void StopFx(Sound sound)
    {
        effectsAudioSource.Stop();
    }

    // Reproduce a sound of the game
    public void PlaySound(Sound sound)
    {
        // Check if the sound is a music or an effect
        if (sound.soundType == Sound.SoundType.MUSIC)
        {
            // Music
            playMusic(sound);
        }
        else if (sound.soundType == Sound.SoundType.FX)
        {
            // Effect
            playFx(sound);
        }
    }

    // Play effect
    private void playFx(Sound sound)
    {
        // Set the clip with the volume and the loop options
        effectsAudioSource.clip = sound.clip;
        effectsAudioSource.volume = sound.volume;
        effectsAudioSource.loop = sound.loop;

        // Play the effect
        effectsAudioSource.Play();
    }

    public void storeMusicStatus()
    {
         PlayerPrefs.SetFloat("MusicTime", musicAudioSource.time);
    }

    public void getMusicStatus()
    {
        musicAudioSource.time = PlayerPrefs.GetFloat("MusicTime");
    }
}