using UnityEngine;
using System.Collections;

public class MoveCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.Move();
    }
}
