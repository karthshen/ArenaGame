using UnityEngine;
using System.Collections;
using InControl;

public class ActorMovingState : ActorState
{
    public ActorMovingState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        return new ActorStandingState();
    }
}
