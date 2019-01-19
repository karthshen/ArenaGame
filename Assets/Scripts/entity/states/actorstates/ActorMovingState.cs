using UnityEngine;
using System.Collections;
using InControl;

public class ActorMovingState : ActorState
{
    Command moveCommand = new MoveCommand();

    public ActorMovingState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if(inputDevice.LeftStickX.Value == 0)
        {
            return new ActorStandingState();
        }
        else
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
            return this;
        }
    }
}
