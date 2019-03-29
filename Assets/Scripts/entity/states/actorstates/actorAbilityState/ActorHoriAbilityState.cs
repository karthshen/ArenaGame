using UnityEngine;
using System.Collections;

public class ActorHoriAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        if (actor.GetName() == "Mage")
        {
            //Thunderbolt animation
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        }
        else if (actor.GetName() == "Warrior")
        {
            //Player Dash Animation
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityHorizAnimation);
        }
        else if (actor.GetName() == "Archer")
        {
            //Triple Arrow animation
        }

        base.PlayAnimation(actor);
    }
}
