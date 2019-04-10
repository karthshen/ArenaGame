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

    public List<Sprite> actorSprites;

    public Image actorImage;

    public List<Image> actorLifes;

    public Text actorName;

    private List<string> actorNames;

    // Start is called before the first frame update
    void Start()
    {
        txtHealth = GetComponent<TextMeshProUGUI>();
        actorNames = new List<string>();
        actorNames.Add("WARRIOR");
        actorNames.Add("MAGE");
        actorNames.Add("ARCHER");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player && playerInputHandler)
        {
            player = playerInputHandler.Actors[playerInputHandler.PlayerNum];
            LoadActorImageAndName();
        }

        for (int i = 3; i > player.RespawnLives; i--)
        {
            if(i>0)
                actorLifes[i-1].enabled = false;
        }

        txtHealth.text = string.Format("{0} %", Mathf.RoundToInt(player.CurrentHealth));
    }

    private void LoadActorImageAndName()
    {
        if(playerInputHandler.PlayerNum == 0)
        {
            actorImage.sprite = actorSprites[(int)GameStageSetting.Player1Selection];
            actorName.text = actorNames[(int)GameStageSetting.Player1Selection];

            //TODO Refactor
            foreach(Image image in actorLifes)
            {
                image.sprite = actorSprites[(int)GameStageSetting.Player1Selection];
            }

            for (int i = 3; i > player.RespawnLives; i--)
            {
                actorLifes[i].enabled = false;
            }
        }
        else if(playerInputHandler.PlayerNum == 1)
        {
            actorImage.sprite = actorSprites[(int)GameStageSetting.Player2Selection];
            actorName.text = actorNames[(int)GameStageSetting.Player2Selection];

            //TODO Refactor
            foreach (Image image in actorLifes)
            {
                image.sprite = actorSprites[(int)GameStageSetting.Player2Selection];
            }

            for (int i = 3; i > player.RespawnLives; i--)
            {
                actorLifes[i].enabled = false;
            }
        }
    }
}
