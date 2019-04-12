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
    public AudioClip boomrang;
    public AudioClip fireball;
    public AudioClip hookback;
    public AudioClip hookshoot;
    public AudioClip shieldslam;
    public AudioClip thunder;
    public AudioClip tornado;
    public AudioClip trap;
    public AudioClip chicken1;
    public AudioClip uiHover;
    public AudioClip uiSelect;
    public AudioClip uiBack;
    public AudioClip battle00;
    public AudioClip battle01;
    public AudioClip menu01;
    public AudioClip teleport;
    public AudioClip fireballHit;

    private AudioClip fakeClip;

    //Random pitch?
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    //Singleton
    public static SoundManager instance = null;

    public void CreateInstance()
    {
        if(instance == null)
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

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
            PlaySound(EffectSource, 1f);
            hasPlayed = true;
        }
    }

    public void PlayEffectWithAudioSource(AudioSource source, AudioClip clip, ref bool hasPlayed, float sourceVolume = 1)
    {
        if(source && clip == footstep && source.clip == footstep)
        {
            if (!source.isPlaying)
                source.Play();
        }

        if (!hasPlayed && source)
        {
            source.clip = clip;
            PlaySound(source, sourceVolume);
            hasPlayed = true;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    private void PlaySound(AudioSource source, float sourceVolume)
    {
        source.volume = sourceVolume;

        source.Play();
    }
}
