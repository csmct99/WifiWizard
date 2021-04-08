using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Sound[] sounds = new Sound[5];
    public static AudioManager instance;

   

    /*
     * Create instance if it doesnt exist
     * and set sound settings
     */
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // create your sounds
        Sound playerStepL = new Sound("PlayerStepL", Resources.Load<AudioClip>("Sound/Footsteps - deleted_user_5093904"), 0.5f, 1f, -0.15f, false);
        Sound playerStepR = new Sound("PlayerStepR", Resources.Load<AudioClip>("Sound/Footsteps - deleted_user_5093904"), 0.5f, 1f, 0.15f, false);
        Sound apPlacementFailure = new Sound("PlaceAPFailure", Resources.Load<AudioClip>("Sound/Failure - GabrielAraujo"), 1f, 1f, 0f, false);
        Sound apPlacementSuccess = new Sound("PlaceAPSuccess", Resources.Load<AudioClip>("Sound/tone beep - pan14"), 1f, 1f, 0f, false);
        Sound jumpLanding = new Sound("PlayerJumpLanding", Resources.Load<AudioClip>("Sound/Footsteps - deleted_user_5093904"), 0.8f, 1f, 0f, false);
        // add them to array
        // remember to set the array size near the start of the file
        sounds[0] = playerStepL;
        sounds[1] = playerStepR;
        sounds[2] = apPlacementFailure;
        sounds[3] = apPlacementSuccess;
        sounds[4] = jumpLanding;

        // set up each sound in the array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.panStereo = s.panStereo;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Play the sound with the specified name
    /// </summary>
    /// <param name="name">The name of the sound. This is the name provided in the Sounds contructor</param>
    public void Play(string name)
    {
        // attempt to find the sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        //  play
        s.source.Play();
    }

    /// <summary>
    /// Play a one shot sound
    /// </summary>
    /// <param name="name">The name of the sound. This is the name provided in the Sounds contructor</param>
    public void PlayOneShot(string name)
    {
        // attempt to find the sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
}
