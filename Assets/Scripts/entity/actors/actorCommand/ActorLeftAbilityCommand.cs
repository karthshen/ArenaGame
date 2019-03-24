using UnityEngine;
using System.Collections;

public class ActorLeftAbilityCommand : Command
{ 
    public override void Execute(AActor actor)
    {
        actor.abilityLeft.AbilityExecute();
    }
}
