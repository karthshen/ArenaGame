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

        return new ActorStandingState();
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,3");
    }
}
