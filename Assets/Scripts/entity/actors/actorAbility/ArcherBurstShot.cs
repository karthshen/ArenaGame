using UnityEngine;
using UnityEditor;

public class ArcherBurstShot : Ability
{
    private const float BURST_FORCE = 1f;
    private float BURST_SPEED = 500f;

    public ArcherBurstShot(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        caster.CurrentEnergy -= AbilityCost;

        float yDirectionInRadian = caster.GetYDirectionInRadian();

        Vector3 chargeMovement = new Vector3(BURST_FORCE * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        caster.GetRigidbody().useGravity = false;

        caster.GetRigidbody().AddForce(-chargeMovement * BURST_SPEED);

        ShootThreeArrow();

        base.AbilityExecute();
    }

    private void ShootArrow(float yModifier)
    {
        caster.AttackCode = System.Guid.NewGuid();

        GameObject archerArrow = Object.Instantiate(Resources.Load("ArcherArrow") as GameObject);
        ArcherArrow arrow = archerArrow.GetComponent<ArcherArrow>();
        arrow.SetOwner(caster);
        arrow.DamageModifier = 2;
        arrow.YModifier = yModifier;
        arrow.ProjectileStart();
    }

    private void ShootThreeArrow()
    {
        ShootArrow(-0.3f);
        ShootArrow(0.0f);
        ShootArrow(0.3f);
    }
}