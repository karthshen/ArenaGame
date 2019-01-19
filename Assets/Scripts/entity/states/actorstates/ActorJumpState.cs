using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    bool bJump = false;

    protected ActorJumpState()
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
}
