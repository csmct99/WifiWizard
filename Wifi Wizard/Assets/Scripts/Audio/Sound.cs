using UnityEngine;


/// <summary>
/// Sound class for use in the AudioManager
/// Create your sounds in the AudioManager
/// </summary>
[System.Serializable]
public class Sound
{
    [Header("Sound Settings")]
    [SerializeField] public string name;
    [SerializeField] public AudioClip clip;
    [Range(0f, 1f)]
    [SerializeField] public float volume = 1f;
    [Range(.1f, 3f)]
    [SerializeField] public float pitch = 1f;
    [Range(-1f, 1f)]
    [SerializeField] public float panStereo = 0f;

    [Tooltip("Specify if this sound should loop")]
    [SerializeField] public bool loop;

    [HideInInspector]
    public AudioSource source;


    /// <summary>
    /// Sound object constructor
    /// </summary>
    /// <param name="name">name of the sound</param>
    /// <param name="clip">clip sound file</param>
    /// <param name="volume">volume of the sound</param>
    /// <param name="pitch">sound pitch</param>
    /// <param name="panStereo">sound stereo pan (left ear or right ear)</param>
    /// <param name="loop">sound loop boolean</param>
    public Sound (string name, AudioClip clip, float volume, float pitch, float panStereo, bool loop)
    {
        this.name = name;
        this.clip = clip;
        this.volume = volume;
        this.pitch = pitch;
        this.panStereo = panStereo;
        this.loop = loop;
    }
}
