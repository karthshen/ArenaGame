using UnityEngine;
using System.Collections;

public class ActorDownAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityDownAnimation);
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.GetActorStat().AbilityDownSound, ref hasSoundPlayed);

        if (!actor.IsGrounded && actor.abilityDown.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
        }

        base.PlayAnimation(actor);
    }
}
