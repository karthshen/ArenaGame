using UnityEngine;
using System.Collections;

public class GrabCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.Grab();
    }
}
