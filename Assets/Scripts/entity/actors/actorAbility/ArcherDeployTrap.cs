using UnityEngine;
using System.Collections;

public class ArcherDeployTrap : Ability
{
    GameObject trap;

    private const int MAX_NUM_TRAPS = 4;

    public ArcherDeployTrap(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsTrapDeployable())
            return;

        caster.CurrentEnergy -= AbilityCost;

        trap = Object.Instantiate(Resources.Load("ArcherTrap")) as GameObject;
        ArcherTrap archerTrap = trap.GetComponent<ArcherTrap>();
        archerTrap.Owner = caster;
        archerTrap.ItemStart();
    }

    private bool IsTrapDeployable()
    {
        ArcherTrap[] traps = GameObject.FindObjectsOfType<ArcherTrap>();
        if (traps.Length >= MAX_NUM_TRAPS)
            return false;
        return true;
    }
}
