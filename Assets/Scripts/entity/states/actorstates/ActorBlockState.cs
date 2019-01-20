using UnityEngine;
using System.Collections;
using InControl;

public class ActorBlockState : ActorState
{
    Command blockCommand = new BlockCommand();

    public ActorBlockState()
    {

    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (!inputDevice.LeftTrigger)
        {
            return new ActorStandingState();
        }
        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        throw new System.NotImplementedException();
    }
}
