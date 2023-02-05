using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    public void Play(string name)
    {
        Debug.Log("gelen " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { Debug.LogWarning("Sound: " + name + " not found"); return; }
        if (!Data.instance.GetSound()) { return; }
        s.source.Play();
    }

}
