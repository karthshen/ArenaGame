using UnityEngine;
using System.Collections;

public class ActorHoriAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        //if (actor.GetName() == "Mage")
        //{
        //    //Thunderbolt animation
        //    actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        //}
        //else if (actor.GetName() == "Warrior")
        //{
        //    //Player Dash Animation
        //    actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        //}
        //else if (actor.GetName() == "Archer")
        //{
        //    //Triple Arrow animation
        //    actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        //}

        actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        SoundManager.instance.PlayEffect(actor.GetActorStat().AbilityHorizSound);

        if (!actor.IsGrounded && actor.abilityLeft.DragInAir && actor.abilityRight.DragInAir)
        {
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG * 2;
        }

        base.PlayAnimation(actor);
    }
}
