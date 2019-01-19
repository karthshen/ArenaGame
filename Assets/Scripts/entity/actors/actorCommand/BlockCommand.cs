using UnityEngine;
using System.Collections;

public class BlockCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.Block();
    }
}
