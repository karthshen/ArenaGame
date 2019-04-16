using UnityEngine;
using System.Collections;

public class ActorNeutralAbilityCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityNeutral.AbilityExecute();
    }
}
