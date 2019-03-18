using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected AActor caster;

    private int abilityCost;

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

    public virtual void AbilityExecute()
    {
        caster.AttackCode = System.Guid.NewGuid();
    }
}
