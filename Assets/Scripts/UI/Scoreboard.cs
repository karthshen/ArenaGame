using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    public List<Sprite> characterIcons;
   
    public Image winnericon;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI winnerTotalDamage;
    public Text winnerPlayerNum;

    public Image losericon;
    public TextMeshProUGUI loserText;
    public TextMeshProUGUI loserTotalDamage;
    public Text loserPlayerNum;

    private float disapperTimer = 5f;

    private bool display = false;

    public bool Enabled
    {
        get
        {
            return enabled;
        }

        set
        {
            enabled = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!enabled)
        {
            display = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (display)
        {
            disapperTimer -= Time.deltaTime;
            if(disapperTimer <= 0)
            {
                SceneManager.LoadScene("Start");
            }
        }
    }

    public void SetWinner(string name)
    {
        if(name == "Warrior")
        {
            winnericon.sprite = characterIcons[0];
            winnerText.text = name;
        }
        if (name == "Mage")
        {
            winnericon.sprite = characterIcons[1];
            winnerText.text = name;
        }
        if (name == "Archer")
        {
            winnericon.sprite = characterIcons[2];
            winnerText.text = name;
        }
    }

    public void SetLoser(string name)
    {
        if (name == "Warrior")
        {
            losericon.sprite = characterIcons[0];
            loserText.text = name;
        }
        if (name == "Mage")
        {
            losericon.sprite = characterIcons[1];
            loserText.text = name;
        }
        if (name == "Archer")
        {
            losericon.sprite = characterIcons[2];
            loserText.text = name;
        }
    }
}
