using UnityEngine;
using System.Collections;

public class ActorDownAbilityState : ActorAbilityState
{
    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetActorStat().AbilityDownAnimation);

        base.PlayAnimation(actor);
    }
}
