using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource EffectSource;
    public AudioSource MusicSource;

    //Audio Clips
    public AudioClip arrow_attack1;
    public AudioClip arrow_attack2;
    public AudioClip footstep;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip staff_attack1;
    public AudioClip staff_attack2;
    public AudioClip sword_attack1;
    public AudioClip sword_attack2;
    public AudioClip sword_attack3;

    public AudioClip fakeClip;

    //Random pitch?
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    //Singleton
    public static SoundManager instance = null;

    //Initialize singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayEffect(AudioClip clip, ref bool hasPlayed)
    {
        if (!hasPlayed)
        {
            EffectSource.clip = clip;
            PlaySound(EffectSource);
            hasPlayed = true;
        }
    }

    public void PlayEffectWithAudioSource(AudioSource source, AudioClip clip, ref bool hasPlayed)
    {
        if(clip == footstep && source.clip == footstep)
        {
            if (!source.isPlaying)
                source.Play();
        }

        if (!hasPlayed)
        {
            source.clip = clip;
            PlaySound(source);
            hasPlayed = true;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    private void PlaySound(AudioSource source)
    {
        if(source.clip == arrow_attack2)
        {
            source.volume = 0.5f;
        }
        else
        {
            source.volume = 1.0f;
        }

        source.Play();
    }
}
