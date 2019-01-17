using UnityEngine;
using System.Collections;

public class ActorJumpState : ActorState
{
    bool bJump = false;

    protected ActorJumpState()
    {
    }

    public override ActorState HandleInput(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    public void SetCanJump(bool bJump)
    {
        this.bJump = bJump;
    }
}
