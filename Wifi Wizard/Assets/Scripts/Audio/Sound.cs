using UnityEngine.Audio;
using UnityEngine;


/*
 * Sound class for use in AudioManager
 * Public variables so AudioManager can access them
 */
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

    [Tooltip("Specify if this sound should loop")]
    [SerializeField] public bool loop;

    [HideInInspector]
    public AudioSource source;
}
