﻿using UnityEngine;
using UnityEditor;

public class ActorTriggerAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityTriggerAnimation);
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.GetActorStat().AbilityTriggerSound, ref hasSoundPlayed);

        if (!actor.IsGrounded && actor.abilityTrigger.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
        }

        base.PlayAnimation(actor);
    }
}