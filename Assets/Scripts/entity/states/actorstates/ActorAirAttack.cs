using UnityEngine;
using System.Collections;
using InControl;

public class ActorAirAttack : ActorState
{
    bool bJump = false;

    protected ActorAirAttack()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        throw new System.NotImplementedException();
    }

    public void SetCanJump(bool bJump)
    {
        this.bJump = bJump;
    }

    protected override void PlayAnimation(AActor actor)
    {
        throw new System.NotImplementedException();
    }
}
