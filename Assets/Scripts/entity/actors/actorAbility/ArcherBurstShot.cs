﻿using UnityEngine;
using UnityEditor;

public class ArcherBurstShot : Ability
{
    private const float BURST_FORCE = 1f;
    private const float BURST_SPEED = 500f;
    private const float BURST_SPEED_AIR = BURST_SPEED / 1.3f;

    public ArcherBurstShot(AActor caster)
    {
        this.caster = caster;
        DragInAir = false;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        caster.CurrentEnergy -= AbilityCost;

        float yDirectionInRadian = caster.GetYDirectionInRadian();

        Vector3 chargeMovement = new Vector3(BURST_FORCE * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        caster.GetRigidbody().useGravity = false;

        if (caster.IsGrounded)
            caster.GetRigidbody().AddForce(-chargeMovement * BURST_SPEED);
        else if(!caster.IsGrounded)
            caster.GetRigidbody().AddForce(-chargeMovement * BURST_SPEED_AIR);

        ShootThreeArrow();

        bool hasPlayed = false;

        AudioSource audioSource = caster.GetAudioSource();

        SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.arrow_attack2, ref hasPlayed, 0.5f);

        base.AbilityExecute();
    }

    private void ShootArrow(float yModifier)
    {
        GameObject archerArrow = Object.Instantiate(Resources.Load("ArcherArrow") as GameObject);
        ArcherArrow arrow = archerArrow.GetComponent<ArcherArrow>();
        arrow.SetOwner(caster);
        arrow.DamageModifier = 0.7f;
        arrow.YModifier = yModifier;
        arrow.ProjectileStart();
    }

    private void ShootThreeArrow()
    {
        ShootArrow(-0.3f);
        ShootArrow(0.0f);
        ShootArrow(0.3f);
    }
}