using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();
    Command blockCommand = new BlockCommand();
    Command grabCommand = new GrabCommand();

    public ActorStandingState()
    {

    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value != 0)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        if ((inputDevice.Action3 || inputDevice.Action4))
        {
            actor.IsGrounded = false;
            jumpCommand.Execute(actor);
            return new ActorJumpState();
        }
        else if (inputDevice.LeftTrigger || inputDevice.LeftBumper)
        {
            blockCommand.Execute(actor);
            return new ActorBlockState();
        }
        else if (inputDevice.RightTrigger || inputDevice.RightBumper)
        {
            grabCommand.Execute(actor);
            return new ActorGrabbingState();
        }

        return this;
    }
}