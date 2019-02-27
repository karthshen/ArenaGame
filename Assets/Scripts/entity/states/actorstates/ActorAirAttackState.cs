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
            return this;
        }

        if (inputDevice.Action2 && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            actor.AttackTimer = AActor.ATTACK_TIMER;
            attackCommand.Execute(actor);
            PlayAnimation(actor);
            actor.attackQueue.Dequeue();
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
            return this;
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
            actor.GetAnimatorController().SetInt("animation,3");
        }
        else if (actor.attackQueue.Peek() == AActor.Combo.Attack1)
        {
            actor.GetAnimatorController().SetInt("animation,4");
        }
        else if (actor.attackQueue.Peek() == AActor.Combo.Attack2)
        {
            actor.GetAnimatorController().SetInt("animation,2");
        }
    }
}
