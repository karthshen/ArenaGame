using UnityEngine;
using System.Collections;

public class ActorUpAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityUpAnimation);

        if (!actor.IsGrounded && actor.abilityDown.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
        }

        base.PlayAnimation(actor);
    }
}
