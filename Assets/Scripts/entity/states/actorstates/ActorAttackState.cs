using UnityEngine;
using System.Collections;
using InControl;

public class ActorAttackState : ActorState
{
    protected ActorAttackState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        throw new System.NotImplementedException();
    }

    protected override void PlayAnimation(AActor actor)
    {
        throw new System.NotImplementedException();
    }
}
