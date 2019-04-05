using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected AActor caster;

    private int abilityCost;

    private bool canCastInAir = true;

    private bool dragInAir = true;

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

    public bool DragInAir
    {
        get
        {
            return dragInAir;
        }

        set
        {
            dragInAir = value;
        }
    }

    public virtual void AbilityExecute()
    {
        caster.AttackCode = System.Guid.NewGuid();
    }
}
