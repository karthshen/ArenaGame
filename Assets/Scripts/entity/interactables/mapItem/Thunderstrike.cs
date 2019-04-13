using UnityEngine;
using System.Collections;

public class Thunderstrike : MapItem
{
    public float disappearTime = 0.4f;

    private AActor owner;

    private bool hasPlayed = false;

    private AudioSource audioSource;

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
        IgnoreOwnerCollision(owner);

        audioSource = GetComponent<AudioSource>();

        gameObject.transform.position = new Vector3(owner.transform.position.x,
            owner.transform.position.y, owner.transform.position.z);

        SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.thunder, ref hasPlayed);
    }

    public override void Interact(AActor actor)
    {
        base.Interact(actor);

        actor.TakeDamageAndFreeze(owner.GetActorStat().AbiltiyPower, 0.5f, owner);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor actor = collision.gameObject.GetComponent<AActor>();
        if (actor)
        {
            Interact(actor);
        }
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            ItemFinish();
        }
    }
}
