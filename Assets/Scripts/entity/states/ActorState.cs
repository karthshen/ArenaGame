using UnityEngine;
using System.Collections;
using InControl;

public abstract class ActorState : EntityState
{
    public abstract ActorState HandleInput(AActor actor, InputDevice inputDevice);
}
