using UnityEngine;
using System.Collections;

public class MageThrowTeleportBolt : Ability
{
    private const int MAX_NUM_BOLT = 1;

    GameObject teleportBolt;
    
    public MageThrowTeleportBolt(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 0;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        IsBoltShootable();

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

    private void IsBoltShootable()
    {
        MageTeleportBolt[] bolts = GameObject.FindObjectsOfType<MageTeleportBolt>();

        foreach (MageTeleportBolt bolt in bolts)
        {
            bolt.RemoveItem();
        }
    }
}
