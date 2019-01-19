using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();

    public ActorStandingState()
    {

    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        moveCommand.Execute(actor);
        return new ActorMovingState();
    }
}