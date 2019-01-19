using UnityEngine;
using UnityEditor;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();

    public ActorStandingState()
    {

    }

    public override ActorState HandleInput(AActor actor)
    {
        moveCommand.Execute(actor);
        return new ActorMovingState();
    }
}