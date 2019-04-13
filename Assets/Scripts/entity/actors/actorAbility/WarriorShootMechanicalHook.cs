using UnityEngine;
using System.Collections;

public class WarriorShootMechanicalHook : Ability
{
    private const int MAX_NUM_CLAW = 1;

    GameObject clawhook;

    public WarriorShootMechanicalHook(AActor caster)
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

        clawhook = Object.Instantiate(Resources.Load("ClawhookWarrior")) as GameObject;
        WarriorMechanicalHook claw = clawhook.GetComponent<WarriorMechanicalHook>();

        if (!claw.CanShootClaw(caster.MoveHorizontal, caster.MoveVertical))
        {
            claw.ProjectileFinish();
        }

        claw.SetOwner(caster);
        claw.ProjectileStart();
    }

    private bool IsClawShootable()
    {
        WarriorMechanicalHook[] hooks = GameObject.FindObjectsOfType<WarriorMechanicalHook>();

        int counter = 0;

        foreach (WarriorMechanicalHook hook in hooks)
        {
            if (hook.GetOwner().GetEntityId() == caster.GetEntityId())
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
