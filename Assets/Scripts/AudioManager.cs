
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


/**
 * Script that controls the reproduction of the sounds
 */

public class AudioManager : MonoBehaviour
{
    // Control if the soundtrack has been changed
    private bool soundtrackChanged;

    // Index of the current soundtrack
    public int currentSoundtrack;

    // Static instance
    public static AudioManager Instance;

    // Reference to the music reproductor
    public AudioSource musicAudioSource;

    // Reference to the effects reproductor
    public AudioSource effectsAudioSource;

    // List with the soundtracks played during the level
    public List<Sound> soundtracks = new List<Sound>();

    // Reference to the music button
    public Image musicButton;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Initialization
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set the current soundtrack to zero and lets the player to change it by default
        currentSoundtrack = 0;
        soundtrackChanged = false;

        // Get the current scene name
        string currentSceneName = GameSceneManager.Instance.GetSceneName();

        // Check if the soundtrack has to be played automatically 
        if (currentSceneName == GameScenes.Intro || currentSceneName == GameScenes.Options)
        {
            // Reproduce the soundtrack from the beginnig
            PlaySound(soundtracks[currentSoundtrack], false);
        }
    }

    // Coroutine that lets the player to change the current soundtrack
    private void AllowNextChangeSoundtrack()
    {
        // Start coroutine to let the player to change the soundtrack
        StartCoroutine(AllowNextChangeSoundtrackCoroutine());
    }

    // Coroutine that lets the player to change the current soundtrack
    private IEnumerator AllowNextChangeSoundtrackCoroutine()
    {
        // Wait two seconds to let the player change the current soundtrack
        yield return new WaitForSeconds(2f);
        // Make the changing soundtrack option available
        soundtrackChanged = false;
        // Change the icon of the music button
        musicButton.GetComponent<ButtonMusicImageManager>().ChangeMusicButtonImage();
    }

    // Play effect
    private void PlayFx(Sound sound)
    {
        // Set the clip with the volume and the loop options
        effectsAudioSource.clip = sound.clip;
        effectsAudioSource.volume = sound.volume;
        effectsAudioSource.loop = sound.loop;

        // Play the effect
        effectsAudioSource.Play();
    }

    // Play soundtrack
    private void PlaySoundtrack(Sound sound, bool musicStatus)
    {
        // Set the clip with the volume and the loop options
        musicAudioSource.clip = sound.clip;
        musicAudioSource.volume = sound.volume;
        musicAudioSource.loop = sound.loop;

        // Play the music reanudating from the previous status
        if (musicStatus)
        {
            // Get the status
            GetSoundtrackStatus();
        }
        else
        {
            // Set the time to zero to start the soundtrack from the beginning
            musicAudioSource.time = 0f;
        }

        // Play the soundtrack
        musicAudioSource.Play();
    }


    // Stop soundtrack
    private void StopSoundtrack(Sound sound, bool musicStatus)
    {
        // Check if the status of the soundtrack must be stored
        if (musicStatus)
        {
            StoreSoundtrackStatus();
        }

        // Stop the current soundtrack
        musicAudioSource.Stop();
    }

    // Stop Fx
    private void StopFx(Sound sound)
    {
        // Stop the current Fx
        effectsAudioSource.Stop();
    }

    // Set the soundtrack status to be continued in the same point
    private void StoreSoundtrackStatus()
    {
        // Store the duration point of the soundtrack
        PlayerPrefs.SetFloat("MusicTime", musicAudioSource.time);
    }

    // Get the soundtrack status to be continued in the same point
    private void GetSoundtrackStatus()
    {
        // Establish the duration point of the soundtrack
        musicAudioSource.time = PlayerPrefs.GetFloat("MusicTime");
    }

    // Get the current soundtrack 
    public Sound GetCurrentSoundtrack()
    {
        return soundtracks[currentSoundtrack];
    }

    // Controls if the soundtrack can be changed
    public void ChangeCurrentSoundtrack()
    {
        // Check if the soundtrack can be changed by the player
        if (!soundtrackChanged)
        {
            // Stop the current soundtrack
            StopSound(soundtracks[currentSoundtrack], false);

            // Change the soundtrack to the next one
            soundtrackChanged = true;
            musicButton.GetComponent<ButtonMusicImageManager>().ChangeMusicButtonImage();
            currentSoundtrack = (currentSoundtrack < soundtracks.Count - 1) ? currentSoundtrack + 1 : 0;

            // Play the soundtrack from the beginning because it's a new one
            PlaySound(soundtracks[currentSoundtrack], false);

            // Let the player to change the soundtrack after two seconds
            AllowNextChangeSoundtrack();
        }
    }

    // Reproduce a sound of the game
    public void StopSound(Sound sound, bool musicStatus)
    {
        // Check if the sound is a music or an effect
        if (sound.soundType == Sound.SoundType.MUSIC)
        {
            // Soundtrack
            StopSoundtrack(sound, musicStatus);
        }
        else if (sound.soundType == Sound.SoundType.FX)
        {
            // Effect
            StopFx(sound);
        }
    }

    // Reproduce a sound of the game
    public void PlaySound(Sound sound, bool musicStatus)
    {
        // Check if the sound is a music or an effect
        if (sound.soundType == Sound.SoundType.MUSIC)
        {
            // Soundtrack
            PlaySoundtrack(sound, musicStatus);
        }
        else if (sound.soundType == Sound.SoundType.FX)
        {
            // Effect
            PlayFx(sound);
        }
    }
}