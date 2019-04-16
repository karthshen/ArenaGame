using UnityEngine;
using System.Collections;

public class DancingArrows : ProjectileItem
{
    [SerializeField]
    private float disappearTime = 2.5f;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        ArcherSwiftArrow[] arrows = GetComponentsInChildren<ArcherSwiftArrow>();
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (ArcherSwiftArrow arrow in arrows)
        {
            arrow.SetOwner(owner);
            arrow.ProjectileStart();
        }

        
        //AActor[] actors = GameObject.FindObjectsOfType<AActor>();

        //foreach (AActor actor in actors)
        //{
        //    Collider[] collidersToIgnore = actor.GetComponentsInChildren<Collider>();
        //    foreach(Collider collider in collidersToIgnore)
        //    {
        //        Physics.IgnoreCollision(GetComponentInChildren<MeshCollider>(), collider);
        //    }
        //}
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;

        if (disappearTime <= 0)
            ProjectileFinish();
    }
}
