using UnityEngine;
using System.Collections;

public class ActorAirAttack : ActorState
{
    bool bJump = false;

    protected ActorAirAttack()
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
