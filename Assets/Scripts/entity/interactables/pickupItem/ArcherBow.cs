using UnityEngine;
using UnityEditor;

public class ArcherBow : PickupItem
{
    GameObject archerArrow;

    float intervalTimer = 0f;
    float arrowPostponedTimer = 1f;

    bool arrowReady = true;

    const float ARROW_POSTPONE_TIME = 0.15f;
    const float ARROW_INTERVAL = 0.5f;

    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);
    }

    public override void UseItem(AActor actor)
    {
        //Shoot the arrow
        if (arrowReady)
        {
            arrowPostponedTimer = 0f;
            arrowReady = false;
            intervalTimer = 0f;
        }
    }

    private void Update()
    {
        if (arrowReady == false)
        {
            intervalTimer += Time.deltaTime;
            if (intervalTimer >= ARROW_INTERVAL)
            {
                arrowReady = true;
            }
        }

        if(arrowPostponedTimer < ARROW_POSTPONE_TIME)
        {
            arrowPostponedTimer += Time.deltaTime;
        }
        else if(arrowPostponedTimer >= ARROW_POSTPONE_TIME && arrowPostponedTimer <1)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        archerArrow = Object.Instantiate(Resources.Load("ArcherArrow") as GameObject);
        ArcherArrow arrow = archerArrow.GetComponent<ArcherArrow>();
        arrow.SetOwner(owner);
        arrow.ProjectileStart();
        arrowPostponedTimer = 1f;
    }
}