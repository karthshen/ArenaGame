﻿using UnityEngine;
using System.Collections;

public class ArcherSwiftArrow : ProjectileItem
{
    public GameObject origin;
    //public GameObject com;

    private Rigidbody rb;

    [SerializeField]
    private float disappearTime = 0.4f;

    [SerializeField]
    private float force = 50f;

    private bool attacked = false;


    public override void ProjectileFinish()
    {
        GameObject fireblast = Object.Instantiate(Resources.Load("ArrowBurst")) as GameObject;

        FireballBurst blast = fireblast.GetComponent<FireballBurst>();

        blast.Owner = owner;

        blast.SetPositionToEnitty(this);

        blast.transform.position = transform.position;

        blast.ItemStart();

        RemoveItem();
    }

    public override void ProjectileStart()
    {
        //Ignore other small lightbombs collision
        ArcherSwiftArrow[] arrows = GameObject.FindObjectsOfType<ArcherSwiftArrow>();
        foreach (ArcherSwiftArrow arrow in arrows)
        {
            if (arrow != this)
                IgnoreEntityCollision(arrow);
        }

        rb = GetComponent<Rigidbody>();

        rb.AddForce((transform.position - origin.transform.position) * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AActor>() && collision.gameObject.GetComponent<AActor>() != owner)
        {
            //Take damage here
            AActor hitActor = collision.gameObject.GetComponent<AActor>();
            hitActor.ClearForceOnActor();
            //hitActor.FreezeTimer = 0f;
            if (!attacked)
            {
                owner.AttackCode = System.Guid.NewGuid();
            }
            hitActor.TakeDamage(owner.GetActorStat().AttackPower / 3.75f, owner);

            bool hasPlayed = false;
            SoundManager.instance.PlayEffectWithAudioSource(hitActor.GetAudioSource(), SoundManager.instance.arrowHit, ref hasPlayed, 0.6f);
            attacked = true;
            //hitActor.ClearForceOnActor();
        }

        ProjectileFinish();
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;

        if (disappearTime <= 0)
        {
            ProjectileFinish();
        }
    }
}
