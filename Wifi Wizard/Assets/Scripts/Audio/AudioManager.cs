using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
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
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /*
     * Play sound with the specified name
     * This is the name of the sound specified in the inspector
     */
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

    /*
     * PlayOneShot with the specified name
     * This is the name of the sound specified in the inspector
     */
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
