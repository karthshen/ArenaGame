using UnityEngine;
using UnityEditor;
using InControl;

public class ActorFreezeState : ActorState
{
    //Stunned time
    float time = 0f;
    float knockingForce = 200f;

    AActor attacker;

    public ActorFreezeState(float time, AActor actor, AActor attacker)
    {
        this.time = time;
        this.attacker = attacker;
        HandleInput(actor, null);
    }

    public ActorFreezeState(float time, AActor actor, AActor attacker, float knockingForce)
    {
        this.time = time;
        this.attacker = attacker;
        this.knockingForce = knockingForce;
        HandleInput(actor, null);
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        if(actor.FreezeTimer == 0f)
        {
            actor.FreezeTimer = time;

            actor.KnockBack(knockingForce, attacker);
        }

        return this;
        
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,8");
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), actor.GetActorStat().DamagedSound, ref hasSoundPlayed);
    }
}