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

    }
}
