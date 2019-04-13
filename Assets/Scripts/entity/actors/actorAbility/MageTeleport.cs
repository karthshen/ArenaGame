using UnityEngine;
using System.Collections;

public class MageTeleport : Ability
{
    MageTeleportBolt bolt;

    public MageTeleport(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsTeleportable())
            return;

        caster.CurrentEnergy -= AbilityCost;

        bolt.OpenTeleport();
    }

    private bool IsTeleportable()
    {
        bolt = GameObject.FindObjectOfType<MageTeleportBolt>();

        if (bolt && bolt.tpState == MageTeleportBolt.TeleportBoltState.Landed)
            return true;
        else
            return false;
    }
}
