using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarriorLightBomb : ProjectileItem
{
    public List<GameObject> smallbombs;

    private float disappearTimer = 3f;

    public override void ProjectileStart()
    {
        foreach(GameObject bomb in smallbombs)
        {
            GameObject createdBomb = Object.Instantiate(Resources.Load("SmallBomb")) as GameObject;
            createdBomb.transform.position = bomb.transform.position;

            SmallLightBomb smallBomb = createdBomb.GetComponent<SmallLightBomb>();

            smallBomb.carrier = gameObject;
            smallBomb.SetOwner(GetOwner());
            smallBomb.ProjectileStart();
        }

        //GameObject [] objects = GameObject.FindObjectsOfType<GameObject>();

        //foreach(GameObject obj in objects)
        //{
        //    if (!obj.GetComponent<SmallLightBomb>())
        //    {
        //        IgnoreGameobjectCollision(obj);
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<SmallLightBomb>())
        {
            IgnoreGameobjectCollision(collision.gameObject);
        }
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;

        if(disappearTimer <= 0)
        {
            ProjectileFinish();
        }
    }
}
