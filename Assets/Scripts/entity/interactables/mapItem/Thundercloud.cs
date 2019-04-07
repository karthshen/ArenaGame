using UnityEngine;
using System.Collections;

public class Thundercloud : MapItem
{
    public float disappearTime = 3.0f;

    [SerializeField]
    private float thunderTimer = 1f;

    private AActor owner;

    GameObject thunderstrike;

    private bool striked;

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
        striked = false;
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void ThunderStrike()
    {
        thunderstrike = Object.Instantiate(Resources.Load("Thunderstrike")) as GameObject;

        Thunderstrike strike = thunderstrike.GetComponent<Thunderstrike>();
        strike.Owner = owner;
        strike.ItemStart();
    }

    private void Update()
    {
        thunderTimer -= Time.deltaTime;
        if(thunderTimer <= 0 && striked == false)
        {
            ThunderStrike();
            striked = true;
        }

        disappearTime -= Time.deltaTime;
        if(disappearTime <= 0)
        {
            ItemFinish();
        }
    }
}
