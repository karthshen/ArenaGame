using UnityEngine;
using System.Collections;
using InControl;

public class ActorAirAttackState : ActorState
{
    Command attackCommand = new AttackCommand();

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (actor.attackQueue.Count == 0)
        {
            actor.AttackTimer = AActor.ATTACK_TIMER_BETWEEN_COMBO;
            ActorJumpState state = new ActorJumpState();
            state.JumpNum = 0;
            state.BAttacked = true;
            return state;
        }

        if (inputDevice.Action2 && actor.AttackTimer < actor.ATTACK_INTERVAL)
        {
            PlayAnimation(actor);
            //Debug.Log("Attack Timer for " + actor.GetName() + " is " + actor.AttackTimer);
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
            attackCommand.Execute(actor);
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if (actor.attackQueue.Count == 0)
        {
            Debug.Log("AttackQueue for " + actor.GetName() + " is empty, playing no attack animation");
        }
        else if (actor.attackQueue.Peek() == AActor.Combo.Attack0)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation1);
            SoundManager.instance.PlayEffect(actor.GetActorStat().AttackSound1);
        }
        else if (actor.attackQueue.Peek() == AActor.Combo.Attack1)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation2);
            SoundManager.instance.PlayEffect(actor.GetActorStat().AttackSound2);
        }
        else if (actor.attackQueue.Peek() == AActor.Combo.Attack2)
        {
            actor.GetAnimatorController().SetInt(actor.GetActorStat().AttackAnimation3);
            SoundManager.instance.PlayEffect(actor.GetActorStat().AttackSound3);
        }
    }
}
