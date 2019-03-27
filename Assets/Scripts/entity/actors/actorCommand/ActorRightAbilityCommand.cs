using UnityEngine;
using System.Collections;

public class ActorRightAbilityCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityRight.AbilityExecute();
    }
}
