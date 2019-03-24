using UnityEngine;
using System.Collections;

public class ActorAbilityUpCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityUp.AbilityExecute();
    }
}
