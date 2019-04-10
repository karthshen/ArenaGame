using UnityEngine;
using UnityEditor;

public class ArcherShootClawhook : Ability
{
    private const int MAX_NUM_CLAW = 1;

    GameObject clawhook;

    public ArcherShootClawhook(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 0;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsClawShootable())
            return;

        caster.CurrentEnergy -= AbilityCost;

        clawhook = Object.Instantiate(Resources.Load("Clawhook")) as GameObject;
        ArcherClawhook claw = clawhook.GetComponent<ArcherClawhook>();

        if(!claw.CanShootClaw(caster.MoveHorizontal, caster.MoveVertical))
        {
            claw.ProjectileFinish();
        }

        claw.SetOwner(caster);
        claw.ProjectileStart();
    }

    private bool IsClawShootable()
    {
        ArcherClawhook[] claws = GameObject.FindObjectsOfType<ArcherClawhook>();

        int counter = 0;

        foreach (ArcherClawhook claw in claws)
        {
            if (claw.GetOwner().GetEntityId() == caster.GetEntityId())
            {
                counter++;
            }
        }

        if (counter >= MAX_NUM_CLAW)
        {
            return false;
        }
        return true;
    }
}