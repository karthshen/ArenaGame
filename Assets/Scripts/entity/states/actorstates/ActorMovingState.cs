﻿using UnityEngine;
using System.Collections;
using InControl;

public class ActorMovingState : ActorStandingState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();
    Command blockCommand = new BlockCommand();
    Command grabCommand = new GrabCommand();

    public ActorMovingState() : base()
    {

    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value == 0)
        {
            return new ActorStandingState();
        }
        else
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        return base.HandleInput(actor, inputDevice);
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,20");
    }
}
