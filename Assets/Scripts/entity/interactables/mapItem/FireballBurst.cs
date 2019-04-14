using UnityEngine;
using System.Collections;

public class FireballBurst : MapItem
{
    private AActor owner;

    private float timer = 2f;

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

    public override void ItemFinish()
    {
        RemoveItem();
    }

    public override void ItemStart()
    {
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
            ItemFinish();
    }
}
