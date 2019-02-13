using UnityEngine;
using System.Collections;
using InControl;

public class ActorDeathState : ActorState
{
    public ActorDeathState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);
        actor.Death();
        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetDeathAnimation());
    }
}
