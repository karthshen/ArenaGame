using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI txtHealth;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Image hurtBar;

    private float speed = 0.3f;

    private float timeLeft;

    public AActor player;

    public InputHandler playerInputHandler;

    public List<Sprite> actorSprites;

    public Image actorImage;

    public List<Image> actorLifes;

   // public Text actorName;

    private List<string> actorNames;

    private float playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        txtHealth = GetComponent<TextMeshProUGUI>();
        timeLeft = 0.5f;
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

        //Yellow health bar changes first

        healthBar.fillAmount = Mathf.RoundToInt(player.CurrentHealth) / 100f;

        if (hurtBar.fillAmount >= healthBar.fillAmount)
        {
            hurtBar.fillAmount -= speed * Time.deltaTime;
        }
        else if (hurtBar.fillAmount <= healthBar.fillAmount)
        {
            hurtBar.fillAmount = healthBar.fillAmount;
        }

         /*
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            //Then red health bar change after 0.5 second
            hurtBar.fillAmount = Mathf.RoundToInt(player.CurrentHealth) / 100f;
            timeLeft = 0.5f;
        }
        */
        txtHealth.text = string.Format("{0} %", Mathf.RoundToInt(player.CurrentHealth));
    }

    private void LoadActorImageAndName()
    {
        if(playerInputHandler.PlayerNum == 0)
        {
            actorImage.sprite = actorSprites[(int)GameStageSetting.Player1Selection];
          //  actorName.text = actorNames[(int)GameStageSetting.Player1Selection];

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
         //   actorName.text = actorNames[(int)GameStageSetting.Player2Selection];

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
