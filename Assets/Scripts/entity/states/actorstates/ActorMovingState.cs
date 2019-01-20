using UnityEngine;
using System.Collections;
using InControl;

public class ActorMovingState : ActorStandingState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();
    Command blockCommand = new BlockCommand();
    Command grabCommand = new GrabCommand();

    public ActorMovingState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if(inputDevice.LeftStickX.Value == 0)
        {
            return new ActorStandingState();
        }
        else if (inputDevice.LeftStickX.Value != 0)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        return base.HandleInput(actor, inputDevice);
    }
}
