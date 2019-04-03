using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI txtHealth;

    public AActor player;

    public InputHandler playerInputHandler;

    // Start is called before the first frame update
    void Start()
    {
        txtHealth = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player && playerInputHandler)
        {
            player = playerInputHandler.Actors[playerInputHandler.PlayerNum];
        }

        txtHealth.text = string.Format("{0} %", Mathf.RoundToInt(player.CurrentHealth));
    }
}
