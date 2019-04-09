using UnityEngine;
using System.Collections;
using InControl;

public class ActorBlockState : ActorState
{
    Command blockCommand = new BlockCommand();

    public ActorBlockState()
    {

    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        if (inputDevice.LeftTrigger.WasReleased || inputDevice.LeftBumper.WasReleased)
        {
            return new ActorStandingState();
        }
        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,33");
        SoundManager.instance.PlayEffectWithAudioSource(actor.GetAudioSource(), SoundManager.instance.sword_attack2, ref hasSoundPlayed);
    }
}
