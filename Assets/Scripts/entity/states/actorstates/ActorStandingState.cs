using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();

    public ActorStandingState()
    {

    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value != 0)
        {
            moveCommand.Execute(actor);
            return new ActorMovingState();
        }
        else if (inputDevice.Action3 || inputDevice.Action4)
        {
            jumpCommand.Execute(actor);
            return new ActorJumpState();
        }

        return this;
    }
}