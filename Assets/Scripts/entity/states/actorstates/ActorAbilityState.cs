using UnityEngine;
using System.Collections;
using InControl;

public class ActorAbilityState : ActorState
{
    public ActorAbilityState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if (actor.GetName() == "Mage")
        {
            actor.GetAnimatorController().SetInt("animation,3");
        }
        else if (actor.GetName() == "Warrior")
        {
            //Player Dash Animation
            actor.GetAnimatorController().SetInt("animation,9");
        }
        else if (actor.GetName() == "Archer")
        {
            //Triple Arrow animation
        }
    }
}
