using UnityEngine;
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

    public abstract void RemoveItem();

    protected void IgnoreOwnerCollision(AActor owner)
    {
        Collider[] collidersToIgnore = owner.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach(Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = owner.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        foreach (Collider selfCollider in colliderInSelf)
        {
            Physics.IgnoreCollision(selfCollider, ownerCollider);
        }
    }
}
