using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public AActor player;

    public List<Image> energyBars;

    public Sprite fullEnergySprite;

    public Sprite emptyEnergySprite;

    // Use this for initialization
    void Start()
    {
        foreach(Image bar in energyBars)
        {
            bar.sprite = emptyEnergySprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (energyBars.Count != player.GetActorStat().MaxEnergy)
        {
            throw new MissingComponentException("Incorrect amount of energy bar for Player: " + player.GetName());
        }

        for (int i = 0; i<energyBars.Count; i++)
        {
            if (i < player.CurrentEnergy)
            {
                energyBars[i].sprite = fullEnergySprite;
            }
            else
            {
                energyBars[i].sprite = emptyEnergySprite;
            }
        }
    }
}
