using UnityEngine;
using UnityEditor;

public class ActorAbilityTriggerCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.abilityTrigger.AbilityExecute();
    }
}