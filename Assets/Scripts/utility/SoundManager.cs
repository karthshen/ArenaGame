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

    //Random pitch?
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    //Singleton
    public static SoundManager instance = null;

    //Initialize singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayEffect(AudioClip clip)
    {
        EffectSource.clip = clip;
        EffectSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
