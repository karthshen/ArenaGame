using UnityEngine;
using System.Collections;
using InControl;

public class ActorAttackState : ActorState
{
    Command attackCommand = new AttackCommand();

    public ActorAttackState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if(actor.attackQueue.Count == 0)
        {
            actor.AttackTimer = AActor.ATTACK_TIMER_BETWEEN_COMBO;
            //Debug.Log("Out of Attack Queue");
            return new ActorStandingState(this.GetType().ToString());
        }

        if (inputDevice.Action2 && actor.AttackTimer < actor.ATTACK_INTERVAL)
        {
            PlayAnimation(actor);
            //Debug.Log("Attack Timer for " + actor.GetName() + " is " + actor.AttackTimer);
            attackCommand.Execute(actor);
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if(actor.attackQueue.Count == 0)
        {
            Debug.Log("AttackQueue for " + actor.GetName() + " is empty, playing no attack animation");
        }

        else if(actor.attackQueue.Peek() == AActor.Combo.Attack0)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation1);
            Debug.Log("AttackQueue Attack0");
        } else if (actor.attackQueue.Peek() == AActor.Combo.Attack1)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation2);
            Debug.Log("AttackQueue Attack1");
        } else if (actor.attackQueue.Peek() == AActor.Combo.Attack2)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation3);
            Debug.Log("AttackQueue Attack2");
        }
    }
}
