using UnityEngine;
using System.Collections;

public abstract class ActorState : EntityState
{
    public abstract ActorState HandleInput(AActor actor);
}
