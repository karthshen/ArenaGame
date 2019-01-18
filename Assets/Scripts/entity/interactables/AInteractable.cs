﻿using UnityEngine;
using System.Collections;

/*
 * Abstract class for interactables that contain *Interaction* which triggers 
 * with an actor tries to interact with it
 */
public abstract class AInteractable : AEntity
{
    //Interaction
    //protected Interaction interaction
    protected new InteractState state;

    public abstract void Interact(AActor actor);

    public abstract void InitializeItem();

    public abstract void RemoveItem();
}
