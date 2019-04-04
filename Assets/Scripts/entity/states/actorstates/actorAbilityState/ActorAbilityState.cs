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

        if(actor.IsGrounded == false)
        {
            ActorJumpState state = new ActorJumpState();
            actor.AbilityCastedInAir = true;
            actor.JumpNum = state.JumpNum;
            return state;
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {

    }
}
