using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    Command jumpCommand = new JumpCommand();

    bool bJump = true;

    public ActorJumpState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if(inputDevice.Action3 || inputDevice.Action4)
        {
            if (bJump)
            {
                jumpCommand.Execute(actor);
                SetCanJump(false);
                return this;
            }
        }

        return this;
    }

    public void SetCanJump(bool bJump)
    {
        this.bJump = bJump;
    }
}
