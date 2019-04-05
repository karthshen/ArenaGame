using UnityEngine;
using System.Collections;

public class ArcherDeployTrap : Ability
{
    GameObject trap;

    public ArcherDeployTrap(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;

        trap = Object.Instantiate(Resources.Load("ArcherTrap")) as GameObject;
        ArcherTrap archerTrap = trap.GetComponent<ArcherTrap>();
        archerTrap.Owner = caster;
        archerTrap.ItemStart();
    }
}
