using UnityEngine;
using System.Collections;

public class ActorDownAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        if (actor.GetName() == "Mage")
        {
            //Thunderbolt animation
            actor.GetAnimatorController().SetInt("animation,3");
        }
        else if (actor.GetName() == "Warrior")
        {
            
        }
        else if (actor.GetName() == "Archer")
        {
            
        }

        base.PlayAnimation(actor);
    }
}
