using UnityEngine;
using System.Collections;

public class WarriorInitialBomb : MapItem
{
    private AActor owner;
    private float freezeTime = 1f;
    [SerializeField]
    private float moveForce = 200f;
    private float moveHorizontal = 0.3f;

    private float disappearTime = 2.0f;

    public AActor Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    public override void ItemStart()
    {
        IgnoreOwnerCollision(owner);

        SetRotationToEntity(owner);

        Rigidbody rb = GetComponent<Rigidbody>();

        float yDirectionInRadian = GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

        rb.AddForce(new Vector3(moveForce * Mathf.Sin(yDirectionInRadian), moveForce, 0));
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;
        if(disappearTime <= 0)
        {
            GameObject lightbomb = Object.Instantiate(Resources.Load("Lightbomb")) as GameObject;

            WarriorLightBomb bomb = lightbomb.GetComponent<WarriorLightBomb>();

            bomb.SetOwner(owner);

            bomb.transform.position = transform.position;

            bomb.ProjectileStart();

            ItemFinish();

            ItemFinish();
        }
    }
}
