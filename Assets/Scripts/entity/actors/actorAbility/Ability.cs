using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected AActor caster;

    private int abilityCost;

    private bool canCastInAir = true;

    public int AbilityCost
    {
        get
        {
            return abilityCost;
        }

        set
        {
            abilityCost = value;
        }
    }

    public bool CanCastInAir
    {
        get
        {
            return canCastInAir;
        }

        set
        {
            canCastInAir = value;
        }
    }

    public virtual void AbilityExecute()
    {
        caster.AttackCode = System.Guid.NewGuid();
    }
}
