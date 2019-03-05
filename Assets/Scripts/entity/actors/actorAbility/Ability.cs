using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected AActor caster;

    public abstract void AbilityExecute();
}
