using UnityEngine;
using System.Collections;

public class ActorAbilityBumperCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityBumper.AbilityExecute();
    }
}
