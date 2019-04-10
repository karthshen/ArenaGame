using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    [SerializeField]
    private float timer;

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    private void Start()
    {
        timer = GameStageSetting.GameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer = 0;

            //For now
            SceneManager.LoadScene("Start");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        int microseconds = (int)((timer * 1000) % 1000);
        if(microseconds >= 100)
        {
            microseconds /= 10;
        }
        int seconds = (int)(timer % 60);
        int minutes = (int)((timer / 60) % 60);

        string timerString = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, microseconds);
        timerText.text = timerString;
    }
}
