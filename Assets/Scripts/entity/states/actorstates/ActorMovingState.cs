using UnityEngine;
using System.Collections;

public class ActorMovingState : ActorState
{
    public ActorMovingState()
    {
    }

    public override ActorState HandleInput(AActor actor)
    {
        return new ActorStandingState();
    }
}
