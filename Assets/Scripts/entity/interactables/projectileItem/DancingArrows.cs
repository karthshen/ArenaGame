using UnityEngine;
using System.Collections;

public class DancingArrows : ProjectileItem
{
    private float disappearTime = 5.0f;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        SmallLightBomb[] arrows = GetComponentsInChildren<SmallLightBomb>();

        foreach (SmallLightBomb arrow in arrows)
        {
            arrow.ProjectileStart();
        }
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;

        if (disappearTime <= 0)
            ProjectileFinish();
    }
}
