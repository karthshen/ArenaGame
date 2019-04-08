﻿using UnityEngine;
using System.Collections;

public class Thunderbolt : ProjectileItem
{
    private float velocity = 10.0f;
    private float moveHorizontal = 1.0f;
    private Vector3 movement;

    private const float DURATION_TIME = 2.0f;

    private float duration_time = 0f;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        SetRotationToEntity(owner);
        float yDirectionInRadian = GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y/2, owner.transform.position.z);
        movement = new Vector3(moveHorizontal * Mathf.Sin(yDirectionInRadian), 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AbiltiyPower / 1.5f, owner);
        }

        if (collision.gameObject.GetComponent<PickupItem>())
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            return;
        }

        ProjectileFinish();
    }

    private void Update()
    {
        transform.Translate(movement * velocity * Time.deltaTime);
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ProjectileFinish();
    }
}
