using UnityEngine;
using System.Collections;

public class StormShield : MapItem
{
    private float duration_time = 0f;
    private float knockingForce = 4.0f;
    private AActor owner;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private const float DURATION_TIME = 1.0f;

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
        gameObject.transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y, 0);
        ParticleSystem ps = gameObject.GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        if (ps)
        {
            ps.playbackSpeed = 4;
        }

        SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.tornado, ref hasPlayed);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void KnockBackCollidedActor(Collider other)
    {
        AActor attackedActor = other.GetComponentInParent<AActor>();
        if (attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
        {
            attackedActor.TakeDamage(owner.GetActorStat().AbiltiyPower / 2, owner);

            attackedActor.KnockBack(knockingForce, owner);

            knockingForce -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ProjectileItem>())
        {
            collision.gameObject.GetComponent<ProjectileItem>().ProjectileFinish();
        }
        else
        {
            IgnoreGameobjectCollision(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        KnockBackCollidedActor(other);
    }

    private void OnTriggerStay(Collider other)
    {
        KnockBackCollidedActor(other);
    }


    private void Update()
    {
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ItemFinish();

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider.size.x < 5f)
        {
            collider.size = new Vector3(collider.size.x + Time.deltaTime * 100, collider.size.y, collider.size.z);
        }
    }
}
