using UnityEngine;
using System.Collections;

public class ActorDownAbilityCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityDown.AbilityExecute();
    }
}
