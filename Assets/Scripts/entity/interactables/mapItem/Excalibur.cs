using UnityEngine;
using System.Collections;

public class Excalibur : MapItem
{
    float disappearTime = 2.0f;

    [SerializeField]
    private float caliburTimer = 0.25f;

    private AActor owner;

    GameObject calibur;

    private bool slashed = false;

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
        slashed = false;
    }


    private void Excaliburrr()
    {
        calibur = Object.Instantiate(Resources.Load("Calibur")) as GameObject;

        Calibur light = calibur.GetComponent<Calibur>();
        light.SetOwner(owner);
        light.ProjectileStart();
    }

    private void Update()
    {
        caliburTimer -= Time.deltaTime;

        if(caliburTimer <= 0 && slashed == false)
        {
            Excaliburrr();
            slashed = true;
        }


        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            ItemFinish();
        }
    }
}
