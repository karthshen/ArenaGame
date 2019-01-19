using UnityEngine;
using System.Collections;

public class JumpCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.Jump();
    }
}
