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
        PlayAnimation(actor);

        if(actor.attackQueue.Count == 0)
        {
            return new ActorStandingState();
        }

        if (inputDevice.Action2.WasPressed && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            actor.attackQueue.Dequeue();
            actor.AttackTimer = AActor.ATTACK_TIMER;
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
            actor.GetAnimatorController().SetInt("animation,3");
        } else if (actor.attackQueue.Peek() == AActor.Combo.Attack1)
        {
            actor.GetAnimatorController().SetInt("animation,4");
        } else if (actor.attackQueue.Peek() == AActor.Combo.Attack2)
        {
            actor.GetAnimatorController().SetInt("animation,2");
        }
    }
}
