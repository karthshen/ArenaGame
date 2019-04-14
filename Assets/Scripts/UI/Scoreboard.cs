using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
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
}
