using UnityEngine;
using System.Collections;

public class MageThrowTeleportBolt : Ability
{
    private const int MAX_NUM_BOLT = 1;

    GameObject teleportBolt;
    
    public MageThrowTeleportBolt(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsBoltShootable())
        {
            return;
        }

        caster.CurrentEnergy -= AbilityCost;
        teleportBolt = Object.Instantiate(Resources.Load("TeleportBolt")) as GameObject;
        MageTeleportBolt bolt = teleportBolt.GetComponent<MageTeleportBolt>();

        if(!bolt.CanShootTeleportBolt(caster.MoveHorizontal, caster.MoveVertical))
        {
            bolt.RemoveItem();
        }

        bolt.SetOwner(caster);
        bolt.ProjectileStart();
    }

    private bool IsBoltShootable()
    {
        MageTeleportBolt[] bolts = GameObject.FindObjectsOfType<MageTeleportBolt>();

        int counter = 0;

        foreach (MageTeleportBolt bolt in bolts)
        {
            if (bolt.GetOwner().GetEntityId() == caster.GetEntityId())
            {
                counter++;
            }
        }

        if (counter >= MAX_NUM_BOLT)
        {
            return false;
        }
        return true;
    }
}
