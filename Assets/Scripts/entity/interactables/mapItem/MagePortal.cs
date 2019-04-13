using UnityEngine;
using System.Collections;

public class MagePortal : MapItem
{
    private float duration_time = 0f;
    private AActor owner;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private const float DURATION_TIME = 2.0f;

    public AActor Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    public override void ItemStart()
    {
        if (owner.IsGrounded)
        {
            gameObject.transform.position = new Vector3(owner.transform.position.x ,
                owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(owner.transform.position.x,
               owner.transform.position.y + owner.transform.lossyScale.y / 2 * 0.2f, owner.transform.position.z);
        }
        ParticleSystem[] pss = gameObject.GetComponentsInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        foreach(ParticleSystem ps in pss)
        {
            ps.playbackSpeed = 2f;
        }

        SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.teleport, ref hasPlayed);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        duration_time += Time.deltaTime;

        if(duration_time >= DURATION_TIME)
        {
            ItemFinish();
        }
    }

}
