﻿using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStunnedState : ActorState
{
    protected ActorStunnedState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        throw new System.NotImplementedException();
    }

    protected override void PlayAnimation(AActor actor)
    {
        throw new System.NotImplementedException();
    }
}